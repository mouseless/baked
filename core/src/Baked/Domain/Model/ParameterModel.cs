using System.Reflection;
using System.Xml;

namespace Baked.Domain.Model;

public record ParameterModel(
    string Name,
    TypeModelReference ParameterTypeReference,
    bool IsOptional,
    object? DefaultValue,
    AttributeCollection CustomAttributes,
    Action<Action<ParameterInfo>> Apply
) : IModel, ICustomAttributesModel, IDocumentedModel
{
    public TypeModel ParameterType => ParameterTypeReference.Model;

    string IModel.Id => Name;

    public XmlNode? Documentation { get; init; }
}