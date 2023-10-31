namespace Do.Domain.Model;

public record DomainModel(
    List<AssemblyModel> Assemblies,
    List<TypeModel> Types
)
{
    static List<AssemblyModel> BuildModel(IAssemblyCollection assemblies)
    {
        var result = new List<AssemblyModel>();
        foreach (var descriptor in assemblies)
        {
            result.Add(new(descriptor.Assembly));
        }

        return result;
    }

    static List<TypeModel> BuildModel(ITypeCollection types)
    {
        var result = new List<TypeModel>();
        foreach (var descriptor in types)
        {
            result.Add(new(descriptor.Type));
        }

        return result;
    }

    public DomainModel(IAssemblyCollection assemblies, ITypeCollection types)
        : this(BuildModel(assemblies), BuildModel(types)) { }
}
