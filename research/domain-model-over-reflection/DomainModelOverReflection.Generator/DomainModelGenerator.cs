using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Text;

namespace DomainModelGenerator;

[Generator]
public class DomainModelGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: (c, _) => c is ClassDeclarationSyntax,
            transform: (n, _) => (ClassDeclarationSyntax)n.Node
        ).Where(m => m is not null);

        var compilation = context.CompilationProvider.Combine(provider.Collect());

        context.RegisterSourceOutput(compilation, (spc, source) => Execute(spc, source.Left, source.Right));
    }

    private void Execute(SourceProductionContext spc, Compilation compilation, ImmutableArray<ClassDeclarationSyntax> types)
    {
        int counter = 0;
        var builder = new StringBuilder();
        foreach (var syntax in types)
        {
            var symbol = compilation.GetSemanticModel(syntax.SyntaxTree).GetDeclaredSymbol(syntax) as INamedTypeSymbol;

            if (symbol is not null && symbol.ContainingNamespace.Name.EndsWith("Business"))
            {
                builder.Append(TypeString(symbol));
                builder.AppendLine(",");
                counter++;
            }
        }

        builder.Length--;

        var code = $$"""
using DomainModelOverReflection.Models.Domain;

namespace {{compilation.AssemblyName}};
                
public class DomainModelWithGeneration : IDomainModel
{
    static TypeModel[] _typeModels = new TypeModel[{{counter}}] 
    {
{{builder}}
    };

    public TypeModel[] TypeModels => _typeModels;
}
""";

        spc.AddSource("DomainModelWithGeneration.g.cs", code);
    }

    private string TypeString(INamedTypeSymbol symbol)
    {
        var typeString = $"""
            new(
                "{symbol.Name}",
                "{GetTypeString(symbol)}",
                {Methods(symbol, symbol.GetMembers().OfType<IMethodSymbol>().Where(m => m.MethodKind == MethodKind.Constructor && !m.IsImplicitlyDeclared).ToList())},
                {Fields(symbol.GetMembers().OfType<IFieldSymbol>().Where(f => !f.IsImplicitlyDeclared).ToList())},
                {Methods(symbol, symbol.GetMembers().OfType<IMethodSymbol>().Where(m => m.MethodKind == MethodKind.Ordinary && !m.IsImplicitlyDeclared).ToList())}
            )
            """;

        return typeString;
    }

    string Fields(List<IFieldSymbol> fields) =>
        !fields.Any() ? "null" : $$"""new FieldModel[{{fields.Count}}] { {{string.Join(", ", fields.Select(Field))}} }""";

    string Field(IFieldSymbol field) =>
        $"""new("{field.Name}", "{GetTypeString(field.Type)}", {(field.DeclaredAccessibility == Accessibility.Private).ToString().ToLowerInvariant()})""";

    string Methods(INamedTypeSymbol target, List<IMethodSymbol> methods) =>
        !methods.Any() ? "null" : $$"""new MethodModel[{{methods.Count}}] { {{string.Join(", ", methods.Select(m => Method(target, m)))}} }""";

    string Method(INamedTypeSymbol target, IMethodSymbol method) =>
        $"""new("{method.Name}", "{GetTypeString(target)}", "{GetTypeString(method.ReturnType)}", {Parameters(method.Parameters)}, {(method.DeclaredAccessibility == Accessibility.Public).ToString().ToLowerInvariant()})""";

    string Parameters(ImmutableArray<IParameterSymbol> parameters) =>
        !parameters.Any() ? "null" : $$"""new ParameterModel[{{parameters.Length}}] { {{string.Join(", ", parameters.Select(Parameter))}} }""";

    string Parameter(IParameterSymbol parameter) =>
        $"""new("{parameter.Name}", "{GetTypeString(parameter.Type)}")""";

    string GetTypeString(ITypeSymbol? symbol) => symbol?.ToDisplayString() ?? string.Empty;
}
