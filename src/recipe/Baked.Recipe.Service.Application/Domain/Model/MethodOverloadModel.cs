using System.Xml;

namespace Baked.Domain.Model;

public record MethodOverloadModel(
    bool IsPublic,
    bool IsFamily,
    bool IsVirtual,
    bool IsStatic,
    bool IsSpecialName,
    ModelCollection<ParameterModel> Parameters,
    TypeModelReference ReturnTypeReference,
    TypeModelReference? DeclaringTypeReference,
    MethodOverloadModel? BaseDefinition
) : MethodBaseModel(
    IsPublic,
    IsFamily,
    IsVirtual,
    false,
    Parameters
), IDocumentedModel
{
    public TypeModel ReturnType => ReturnTypeReference.Model;
    public TypeModel? DeclaringType => DeclaringTypeReference?.Model;

    public XmlNode? Documentation { get; init; }
}