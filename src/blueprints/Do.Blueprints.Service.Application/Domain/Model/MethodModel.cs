namespace Do.Domain.Model;

public record MethodModel(
    string Name,
    TypeModel ReturnType,
    bool IsPublic,
    ModelCollection<ParameterModel> Parameters
) : IModel;


