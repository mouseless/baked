namespace Do.Domain.Model;

public abstract record MethodBaseModel(
    bool IsPublic,
    bool IsFamily,
    bool IsVirtual,
    bool IsConstructor,
    ModelCollection<ParameterModel> Parameters
);