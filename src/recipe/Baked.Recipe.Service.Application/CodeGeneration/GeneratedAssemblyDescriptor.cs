using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace Baked.CodeGeneration;

public record GeneratedAssemblyDescriptor(string Name)
{
    public List<string> Codes { get; } = [];
    public CSharpCompilationOptions CompilationOptions { get; set; } = new(OutputKind.DynamicallyLinkedLibrary);
    public List<Assembly> References { get; } = [];
}