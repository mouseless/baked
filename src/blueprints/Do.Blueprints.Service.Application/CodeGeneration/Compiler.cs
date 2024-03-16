using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using System.Text;

namespace Do.CodeGeneration;

public class Compiler(CompilerOptions _compilerOptions)
{
    readonly List<string> _codes = new();
    readonly Dictionary<string, MetadataReference> _references = new();

    public void AddCode(string code) => _codes.Add(code);

    void AddReference(Assembly assembly)
    {
        if (_references.ContainsKey(assembly.Location)) { return; }

        _references.Add(assembly.Location, MetadataReference.CreateFromFile(assembly.Location));
        foreach (var referencedAssembly in assembly.GetReferencedAssemblies())
        {
            AddReference(Assembly.Load(referencedAssembly));
        }
    }

    public Assembly Compile()
    {
        foreach (var assembly in _compilerOptions.References)
        {
            AddReference(assembly);
        }

        var compilation = CSharpCompilation.Create(
            Path.GetRandomFileName(),
            syntaxTrees: _codes.Select(c => CSharpSyntaxTree.ParseText(c)),
            references: _references.Values,
            options: new(OutputKind.DynamicallyLinkedLibrary)
        );

        using var ms = new MemoryStream();
        var result = compilation.Emit(ms);
        if (!result.Success)
        {
            var failures = result.Diagnostics.Where(diagnostic =>
                diagnostic.IsWarningAsError ||
                diagnostic.Severity == DiagnosticSeverity.Error
            );
            var errors = new StringBuilder();
            foreach (var diagnostic in failures)
            {
                errors.AppendLine($"{diagnostic.Location.GetLineSpan()} - {diagnostic.Id}: {diagnostic.GetMessage()}");
                errors.AppendLine();
            }

            throw new Exception($"{errors}");
        }

        ms.Seek(0, SeekOrigin.Begin);

        return Assembly.Load(ms.ToArray());
    }
}
