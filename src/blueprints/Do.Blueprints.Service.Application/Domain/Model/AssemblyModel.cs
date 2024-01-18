using System.Reflection;

namespace Do.Domain.Model;

public class AssemblyModel(Assembly _assembly)
    : IModel
{
    public string Name { get; } = _assembly.GetName().Name ?? string.Empty;
    public string FullName { get; } = _assembly.FullName ?? string.Empty;
    public string Id => FullName;

    public void Apply(Action<Assembly> action) => action(_assembly);
}