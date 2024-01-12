namespace Do.Domain.Model;

public record ParameterModel(
    string Name,
    TypeModel ParameterType
) : IModel;

