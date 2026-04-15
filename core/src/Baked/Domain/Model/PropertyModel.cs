using System.Xml;

namespace Baked.Domain.Model;

public record PropertyModel(
    string Name,
    TypeModelReference PropertyTypeReference,
    bool IsPublic,
    bool IsVirtual,
    bool IsAutoProperty,
    AttributeCollection CustomAttributes
) : IModel, ICustomAttributesModel, IDocumentedModel
{
    public TypeModel PropertyType => PropertyTypeReference.Model;

    string IModel.Id { get; } = Name;
    AttributeTargets ICustomAttributesModel.Target => AttributeTargets.Property;

    public XmlNode? Documentation { get; set; }
}