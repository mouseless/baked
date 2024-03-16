using System.Reflection;

namespace Do.CodeGeneration;

public record CompilerOptions()
{
    public List<Assembly> References { get; } = new();
}
