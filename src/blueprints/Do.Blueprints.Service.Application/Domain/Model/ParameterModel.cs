namespace Do.Domain.Model;

public record ParameterModel(
    string Name,
    TypeModel ParameterType,
    bool IsOptional,
    object? DefaultValue
) : IModel
{
    string IModel.Id { get; } = Name;
}

