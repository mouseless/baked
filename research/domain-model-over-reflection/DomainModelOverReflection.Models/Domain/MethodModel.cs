namespace DomainModelOverReflection.Models.Domain;

public record MethodModel(
    string Name,
    Type Target,
    Type ReturnType,
    List<ParameterModel> Parameters,
    bool IsPublic
);
