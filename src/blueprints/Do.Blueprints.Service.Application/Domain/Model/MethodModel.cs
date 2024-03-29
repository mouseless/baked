namespace Do.Domain.Model;

public record MethodModel(
    string Name,
    bool IsPublic,
    bool IsProtected,
    bool IsVirtual,
    TypeModelReference? ReturnTypeReference,
    AttributeCollection CustomAttributes,
    ModelCollection<ParameterModel> Parameters
) : MethodBaseModel(
    Name,
    IsPublic,
    IsProtected,
    IsVirtual,
    false,
    ReturnTypeReference,
    CustomAttributes,
    Parameters
);
