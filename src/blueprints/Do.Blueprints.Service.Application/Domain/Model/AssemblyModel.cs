using System.Reflection;

namespace Do.Domain.Model;

public class AssemblyModel(Assembly _assembly)
    : IModel
{
    public string Name { get; } = _assembly.FullName ?? string.Empty;
    public string Id => Name;

    public void Apply(Action<Assembly> action) => action(_assembly);
}