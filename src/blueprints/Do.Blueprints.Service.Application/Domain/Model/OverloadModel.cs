namespace Do.Domain.Model;

public record OverloadModel(
    bool IsPublic,
    bool IsProtected,
    bool IsVirtual,
    ModelCollection<ParameterModel> Parameters,
    ModelCollection<TypeModel> CustomAttributes,
    TypeModel? ReturnType = default
);
