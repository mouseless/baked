namespace Baked.Domain.Model;

public record PropertyModel(
    string Name,
    TypeModelReference PropertyTypeReference,
    bool IsPublic,
    bool IsVirtual,
    AttributeCollection CustomAttributes
) : IModel, ICustomAttributesModel
{
    public TypeModel PropertyType => PropertyTypeReference.Model;

    string IModel.Id { get; } = Name;
}