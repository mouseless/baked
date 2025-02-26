using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using System.Text;

namespace Baked.CodeGeneration;

public class Compiler(GeneratedAssemblyDescriptor _descriptor)
{
    readonly Dictionary<string, MetadataReference> _references = new();

    public string Compile(string assemblyLocation,
        string? assemblyName = default
    )
    {
        assemblyName ??= $"Baked.g.{_descriptor.Name}";

        _descriptor.AddCode(string.Join(
            Environment.NewLine,
            _descriptor.CompilationOptions.Usings.Select(u => $"global using global::{u};")
        ));

        var codes = string.Join(Environment.NewLine, _descriptor.Codes);
        var dllPath = Path.Combine(Path.Combine(assemblyLocation, $"{assemblyName}.dll"));
        var hashFilePath = $"{dllPath}.hash";
        if (!CodeGenerationExtensions.RequiresUpdate(codes, dllPath, hashFilePath))
        {
            return dllPath;
        }

        using (var file = new FileStream(dllPath, FileMode.Create))
        {
            using var ms = CreateCSharpCompilation(assemblyName);
            ms.WriteTo(file);
        }

        using (var hashfile = new FileStream(hashFilePath, FileMode.Create))
        {
            hashfile.Write(codes.ToSHA256());
        }

        File.WriteAllText($"{dllPath}.cs", codes);

        return dllPath;

    }

    MemoryStream CreateCSharpCompilation(string assemblyName)
    {
        foreach (var assembly in _descriptor.References)
        {
            AddReference(assembly);
        }

        var compilation = CSharpCompilation.Create(
            assemblyName: assemblyName,
            syntaxTrees: _descriptor.Codes.Select(c => CSharpSyntaxTree.ParseText(c)),
            references: _references.Values,
            options: _descriptor.CompilationOptions
        );

        var ms = new MemoryStream();
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

        return ms;
    }

    void AddReference(Assembly assembly)
    {
        if (_references.ContainsKey(assembly.Location)) { return; }

        _references.Add(assembly.Location, MetadataReference.CreateFromFile(assembly.Location));
        foreach (var referencedAssembly in assembly.GetReferencedAssemblies())
        {
            AddReference(Assembly.Load(referencedAssembly));
        }
    }
}