namespace DomainModelOverReflection.Models.Domain;

public record MethodModel(
    string Name,
    TypeModel Target,
    Type ReturnType,
    List<ParameterModel> Parameters,
    bool IsPublic
);
