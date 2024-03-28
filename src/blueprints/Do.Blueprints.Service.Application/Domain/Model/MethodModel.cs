namespace Do.Domain.Model;

public record MethodModel(
    MethodGroupModel Group,
    bool IsPublic,
    bool IsProtected,
    bool IsVirtual,
    TypeModel? ReturnType
) : MethodBaseModel(
    IsPublic,
    IsProtected,
    IsVirtual,
    false,
    ReturnType
);
