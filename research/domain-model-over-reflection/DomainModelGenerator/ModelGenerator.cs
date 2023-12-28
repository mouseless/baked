using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Text;

namespace DomainModelGenerator;

[Generator]
public class ModelGenerator : IIncrementalGenerator
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
        var builder = new StringBuilder();
        foreach (var syntax in types)
        {
            var symbol = compilation.GetSemanticModel(syntax.SyntaxTree).GetDeclaredSymbol(syntax) as INamedTypeSymbol;

            if (symbol is not null && symbol.ContainingNamespace.Name.EndsWith("Business"))
            {
                builder.Append(TypeString(symbol));
                builder.AppendLine(",");
            }
        }

        builder.Length--;

        var code = $$"""
using DomainModelOverReflection.Models.Domain;
using System;
using System.Collections.Generic;

namespace {{compilation.AssemblyName}};
                
public class DomainModel : IDomainModel
{
    static TypeModel[] _typeModels = new TypeModel[] 
    {
{{builder}}
    };

    public TypeModel[] TypeModels => _typeModels;
}
""";

        spc.AddSource("DomainModel.g.cs", code);
    }

    private string TypeString(INamedTypeSymbol symbol)
    {
        var typeString = $"""
            new(
                "{symbol.Name}",
                "{GetTypeString(symbol)}",
                {Methods(symbol, symbol.GetMembers().OfType<IMethodSymbol>().Where(m => m.MethodKind == MethodKind.Constructor).ToList())},
                {Fields(symbol.GetMembers().OfType<IFieldSymbol>().ToList())},
                {Methods(symbol, symbol.GetMembers().OfType<IMethodSymbol>().Where(m => m.MethodKind == MethodKind.Ordinary).ToList())}
            )
            """;

        return typeString;
    }

    string Fields(List<IFieldSymbol> fields) =>
        !fields.Any() ? "Array.Empty<FieldModel>()" : $$"""new FieldModel[] { {{string.Join(", ", fields.Select(Field))}} }""";

    string Field(IFieldSymbol field) =>
        $"""new("{field.Name}", "{GetTypeString(field.Type)}", {(field.DeclaredAccessibility == Accessibility.Private).ToString().ToLowerInvariant()})""";

    string Methods(INamedTypeSymbol target, List<IMethodSymbol> methods) =>
        !methods.Any() ? "Array.Empty<MethodModel>()" : $$"""new MethodModel[] { {{string.Join(", ", methods.Select(m => Method(target, m)))}} }""";

    string Method(INamedTypeSymbol target, IMethodSymbol method) =>
        $"""new("{method.Name}", "{GetTypeString(target)}", "{GetTypeString(method.ReturnType)}", {Parameters(method.Parameters)}, {(method.DeclaredAccessibility == Accessibility.Public).ToString().ToLowerInvariant()})""";

    string Parameters(ImmutableArray<IParameterSymbol> parameters) =>
        !parameters.Any() ? "Array.Empty<ParameterModel>()" : $$"""new ParameterModel[] { {{string.Join(", ", parameters.Select(Parameter))}} }""";

    string Parameter(IParameterSymbol parameter) =>
        $"""new("{parameter.Name}", "{GetTypeString(parameter.Type)}")""";

    string GetTypeString(ITypeSymbol? symbol) => symbol?.ToDisplayString() ?? string.Empty;
}
