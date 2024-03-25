namespace Do.Domain.Model;

public record ParameterModel(
    OverloadModel Overload,
    string Name,
    TypeModel ParameterType,
    bool IsOptional,
    object? DefaultValue,
    AttributeCollection CustomAttributes
) : IModel
{
    public bool HasAttribute<T>() where T : Attribute =>
     CustomAttributes.ContainsKey(typeof(T));

    string IModel.Id { get; } = Name;
}

