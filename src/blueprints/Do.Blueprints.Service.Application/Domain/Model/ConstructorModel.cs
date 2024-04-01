namespace Do.Domain.Model;

public record ConstructorModel(
    bool IsPublic,
    bool IsProtected,
    AttributeCollection CustomAttributes,
    ModelCollection<ParameterModel> Parameters
) : MethodBaseModel(
    "ctor",
    IsPublic,
    IsProtected,
    false,
    true,
    default,
    CustomAttributes,
    Parameters
);
