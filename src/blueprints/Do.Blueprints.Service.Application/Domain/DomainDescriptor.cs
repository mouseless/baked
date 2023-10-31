using System.Reflection;

namespace Do.Domain;

public class DomainDescriptor
{
    public List<Assembly> Assemblies { get; } = new();
    public List<Type> Types { get; } = new();

    public void AddAssembly<T>() => AddAssembly(typeof(T));
    public void AddAssembly(Type type) => Assemblies.Add(type.Assembly);

    public void AddType<T>() => AddType(typeof(T));
    public void AddType(Type type) => Types.Add(type);
}
