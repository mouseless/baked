namespace Do.Domain.Model;

public record DomainModel(
    ModelCollection<AssemblyModel> Assemblies,
    ModelCollection<TypeModel> Types,
    HashSet<string> DomainTypeIds
);
