using System.Reflection;

namespace Do.Domain.Model;

public record AssemblyModel(
    string Name
) : IModel
{
    readonly Assembly _assembly = default!;

    public AssemblyModel(Assembly assembly)
        : this(assembly.FullName ?? string.Empty)
    {
        _assembly = assembly;
    }

    public string Id => Name;

    internal void Apply(Action<Assembly> action) => action(_assembly);
}
