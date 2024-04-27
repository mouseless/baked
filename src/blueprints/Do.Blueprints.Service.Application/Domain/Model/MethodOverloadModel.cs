namespace Do.Domain.Model;

public record MethodOverloadModel(
    bool IsPublic,
    bool IsFamily,
    bool IsVirtual,
    bool IsStatic,
    bool IsSpecialName,
    ModelCollection<ParameterModel> Parameters,
    TypeModelReference ReturnTypeReference
) : MethodBaseModel(
    IsPublic,
    IsFamily,
    IsVirtual,
    false,
    Parameters
)
{
    public TypeModel ReturnType => ReturnTypeReference.Model;
}