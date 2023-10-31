using Do.Domain.Model;
using System.Reflection;

namespace Do.Domain;

public class DomainModelBuilder
{
    readonly DomainDescriptor _descriptor;

    public DomainModelBuilder(DomainDescriptor descriptor)
    {
        _descriptor = descriptor;
    }

    public DomainModel Build() => new(Assemblies: GenerateAssemblies(_descriptor.Assemblies), Types: GenerateTypes(_descriptor.Types));

    static List<AssemblyModel> GenerateAssemblies(List<Assembly> assemblies)
    {
        var result = new List<AssemblyModel>();
        foreach (var assembly in assemblies)
        {
            result.Add(new(assembly));
        }

        return result;
    }

    static List<TypeModel> GenerateTypes(List<Type> types)
    {
        var result = new List<TypeModel>();
        foreach (var type in types)
        {
            result.Add(new(type));
        }

        return result;
    }
}
