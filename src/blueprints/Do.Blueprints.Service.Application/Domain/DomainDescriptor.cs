using System.Reflection;

namespace Do.Domain;

public class DomainDescriptor
{
    internal List<Assembly> AssemblyList { get; } = new();
    internal List<Type> IncludedTypes { get; } = new();

    public void IncludeType<T>() => IncludeType(typeof(T));

    public void IncludeType(Type type)
    {
        IncludedTypes.Add(type);
    }
}
