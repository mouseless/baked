using System.Reflection;

namespace Do.Domain.Model;

public record PropertyModel(
    string Name,
    TypeModelReference PropertyTypeReference,
    bool IsPublic,
    bool IsVirtual,
    AttributeCollection CustomAttributes,
    Action<Action<PropertyInfo>> Apply
) : IModel, ICustomAttributesModel
{
    public TypeModel PropertyType => PropertyTypeReference.Model;

    string IModel.Id { get; } = Name;
}