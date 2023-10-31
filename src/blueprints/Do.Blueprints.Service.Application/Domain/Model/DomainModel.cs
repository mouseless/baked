using System.Reflection;

namespace Do.Domain.Model;

public record DomainModel(
    List<AssemblyModel> Assemblies,
    List<TypeModel> Types
)
{
    static List<AssemblyModel> BuildModel(List<Assembly> assemblies)
    {
        var result = new List<AssemblyModel>();
        foreach (var assembly in assemblies)
        {
            result.Add(new(assembly));
        }

        return result;
    }

    static List<TypeModel> BuildModel(List<Type> types)
    {
        var result = new List<TypeModel>();
        foreach (var type in types)
        {
            result.Add(new(type));
        }

        return result;
    }

    public DomainModel(DomainDescriptor descriptor)
        : this(BuildModel(descriptor.Assemblies), BuildModel(descriptor.Types)) { }
}
