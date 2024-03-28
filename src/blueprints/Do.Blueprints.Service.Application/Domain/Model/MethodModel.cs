namespace Do.Domain.Model;

public record MethodModel(
    string Name,
    bool IsPublic,
    bool IsProtected,
    bool IsVirtual,
    TypeModel? ReturnType,
    AttributeCollection CustomAttributes,
    ModelCollection<ParameterModel> Parameters
) : MethodBaseModel(
    Name,
    IsPublic,
    IsProtected,
    IsVirtual,
    false,
    ReturnType,
    CustomAttributes,
    Parameters
);
