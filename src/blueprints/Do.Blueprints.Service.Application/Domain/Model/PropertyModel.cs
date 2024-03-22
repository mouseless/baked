namespace Do.Domain.Model;

public record PropertyModel(
    string Name,
    TypeModel PropertyType,
    bool IsPublic,
    bool IsVirtual
) : IModelWithMetadata
{
    public ModelCollection<TypeModel> CustomAttributes { get; private set; } = default!;

    public bool Has<T>() where T : Attribute =>
        CustomAttributes.Contains(TypeModel.IdFrom(typeof(T)));

    string IModel.Id { get; } = Name;
}
