namespace Do.Domain.Model;

public record PropertyModel(
    string Name,
    TypeModelReference PropertyTypeReference,
    bool IsPublic,
    bool IsVirtual,
    AttributeCollection CustomAttributes
) : IModel, ICustomAttributesModel
{
    public TypeModel PropertyType => PropertyTypeReference.Model;

    public bool Has<T>() where T : Attribute =>
        CustomAttributes.Contains<T>();

    string IModel.Id { get; } = Name;
}
