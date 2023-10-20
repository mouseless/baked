using System.Reflection;

namespace Do.Domain;

public class DomainDescriptor
{
    public List<Assembly> AssemblyList { get; } = new();
    public List<Type> IncludedTypes { get; } = new();

    public void AddAssemblyOfType<T>() => AddAssemblyOfType(typeof(T));
    public void AddAssemblyOfType(Type type) => AssemblyList.Add(type.Assembly);

    public void AddType<T>() => AddType(typeof(T));
    public void AddType(Type type) => IncludedTypes.Add(type);
}
