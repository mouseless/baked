namespace DomainModelOverReflection.Models.Domain;

public record MethodModel(string Name, Type ReturnType, List<ParameterModel> Parameters);
