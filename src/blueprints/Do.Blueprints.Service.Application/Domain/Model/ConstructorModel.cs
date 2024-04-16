namespace Do.Domain.Model;

public record ConstructorModel(
    bool IsPublic,
    bool IsFamily,
    ModelCollection<ParameterModel> Parameters
) : MethodBaseModel(
    IsPublic,
    IsFamily,
    false,
    true,
    Parameters
);