namespace Do.Domain.Model;

public record ParameterModel(
    MethodModel Overload,
    string Name,
    TypeModel ParameterType,
    bool IsOptional,
    object? DefaultValue,
    AttributeCollection CustomAttributes
) : IModel
{
    public bool Has<T>() where T : Attribute =>
     CustomAttributes.ContainsKey(typeof(T));

    string IModel.Id { get; } = Name;
}

