namespace Do.Domain.Model;

public record ParameterModel(
    string Name,
    TypeModel ParameterType,
    bool IsOptional,
    ValueModel DefaultValue
) : IModel
{
    public ParameterModel(string name, TypeModel parameterType, bool isOptional, object? defaultValue)
        : this(name, parameterType, isOptional, new ValueModel(parameterType, defaultValue))
    { }

    public string Id { get; } = $"{ParameterType.Id} {Name}";
}

