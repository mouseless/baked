namespace Do.Domain.Model;

public record PropertyModel(
    string Name,
    TypeModel PropertyType,
    bool IsPublic,
    bool IsVirtual,
    ModelCollection<TypeModel> CustomAttributes
) : IModelWithMetadata
{
    public bool HasAttribute<T>() where T : Attribute =>
        CustomAttributes.Contains(TypeModel.IdFrom(typeof(T)));

    string IModel.Id { get; } = Name;
}
