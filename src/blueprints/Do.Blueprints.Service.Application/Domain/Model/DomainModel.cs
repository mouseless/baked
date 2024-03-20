namespace Do.Domain.Model;

public record DomainModel(
    ModelCollection<AssemblyModel> Assemblies,
    TypeModelCollection Types
)
{
    internal ModelCache<TypeModel> TypeCache { get; } = [];

    public ModelCollection<TypeModel> GetTypes<T>() =>
        TypeCache.GetOrEmpty(TypeModel.IdFrom(typeof(T)));
}
