namespace Do.Domain.Model;

public record PropertyModel(
    string Name,
    TypeModelReference PropertyTypeReference,
    bool IsPublic,
    bool IsVirtual,
    AttributeCollection CustomAttributes
) : ICustomAttributesModel, IKeyedModel
{
    public TypeModel PropertyType => PropertyTypeReference.Model;

    public bool Has<T>() where T : Attribute =>
        CustomAttributes.ContainsKey<T>();

    string IKeyedModel.Id { get; } = Name;
}
