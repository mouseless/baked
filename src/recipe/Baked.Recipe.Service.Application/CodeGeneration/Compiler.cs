using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using System.Text;

namespace Baked.CodeGeneration;

public class Compiler(GeneratedAssemblyDescriptor _descriptor)
{
    readonly Dictionary<string, MetadataReference> _references = new();

    void AddReference(Assembly assembly)
    {
        if (_references.ContainsKey(assembly.Location)) { return; }

        _references.Add(assembly.Location, MetadataReference.CreateFromFile(assembly.Location));
        foreach (var referencedAssembly in assembly.GetReferencedAssemblies())
        {
            AddReference(Assembly.Load(referencedAssembly));
        }
    }

    public Assembly Compile(
        string? assemblyLocation = default,
        string? assemblyName = default
    )
    {
        assemblyName ??= $"Baked.g.{_descriptor.Name}";

        foreach (var assembly in _descriptor.References)
        {
            AddReference(assembly);
        }

        _descriptor.AddCode(string.Join(
            Environment.NewLine,
            _descriptor.CompilationOptions.Usings.Select(u => $"global using global::{u};")
        ));

        var compilation = CSharpCompilation.Create(
            assemblyName: assemblyName,
            syntaxTrees: _descriptor.Codes.Select(c => CSharpSyntaxTree.ParseText(c)),
            references: _references.Values,
            options: _descriptor.CompilationOptions
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
                errors.AppendLine();
                errors.AppendLine(diagnostic.GetMessage());
                errors.AppendLine();
                errors.AppendLine(diagnostic.FindClosestScopedCode());
                errors.AppendLine();
            }

            throw new Exception($"{errors}");
        }

        ms.Seek(0, SeekOrigin.Begin);

        if (assemblyLocation is not null)
        {
            using (var file = new FileStream(Path.Combine(assemblyLocation, $"{assemblyName}.dll"), FileMode.Create))
            {
                ms.WriteTo(file);
            }

            return Assembly.LoadFrom(Path.Combine(assemblyLocation, $"{assemblyName}.dll"));
        }

        return Assembly.Load(ms.ToArray());
    }
}