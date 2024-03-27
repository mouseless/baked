namespace Do.Domain.Model;

public record ConstructorModel(
    ConstructorGroupModel Group,
    bool IsPublic,
    bool IsProtected,
    TypeModel? ReturnType = default
) : MethodBaseModel(
    IsPublic,
    IsProtected,
    false,
    true,
    ReturnType
);
