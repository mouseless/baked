namespace Do.Domain.Model;

public record ParameterModel(
    MethodBaseModel Method,
    string Name,
    TypeModel ParameterType,
    bool IsOptional,
    object? DefaultValue,
    AttributeCollection CustomAttributes
) : IMemberModel
{
    public bool Has<T>() where T : Attribute =>
        CustomAttributes.ContainsKey<T>();

    string IModel.Id { get; } = Name;
}

