using System.Reflection;

namespace Baked.CodeGeneration;

public interface ICodeTemplate
{
    IEnumerable<Assembly> References { get; }

    IEnumerable<string> Render();
}