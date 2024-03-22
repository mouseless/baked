namespace Do.Domain.Model;

public record DomainModel(
    ModelCollection<AssemblyModel> Assemblies,
    TypeModelCollection Types
);

public record DomainDescriptor(
    ModelCollection<AssemblyModel> Assemblies,
    ModelCollection<TypeModel> Types
);
