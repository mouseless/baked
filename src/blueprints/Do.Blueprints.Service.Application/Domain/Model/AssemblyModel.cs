using System.Reflection;

namespace Do.Domain.Model;

public class AssemblyModel(Assembly _assembly)
    : IModel
{
    public string Name { get; } = _assembly.GetName().Name ?? string.Empty;
    public string FullName { get; } = _assembly.FullName ?? string.Empty;
    public AttributeCollection CustomAttributes { get; private set; } = default!;

    string IModel.Id => FullName;

    public void Apply(Action<Assembly> action) => action(_assembly);

    public bool HasAttribute<T>() where T : Attribute
    {
        throw new NotImplementedException();
    }
}