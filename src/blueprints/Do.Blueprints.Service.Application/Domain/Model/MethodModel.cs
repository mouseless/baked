namespace Do.Domain.Model;

public record MethodModel(
    string Name,
    TypeModel? ReturnType,
    List<ParameterModel> Parameters,
    bool IsPublic
);


