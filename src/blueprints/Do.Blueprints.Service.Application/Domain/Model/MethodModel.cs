namespace Do.Domain.Model;

public record MethodModel(string Name, Type ReturnType, bool IsPublic, bool IsConstructor,
    Type[]? GenericArguements = default,
    List<ParameterModel>? Parameters = default
);

