namespace Do.Domain.Model;

public record ParameterModel(
    OverloadModel Overload,
    string Name,
    TypeModel ParameterType,
    bool IsOptional,
    object? DefaultValue,
    ModelCollection<TypeModel> CustomAttributes
) : IModelWithMetadata
{
    public bool HasAttribute<T>() where T : Attribute =>
     CustomAttributes.Contains(TypeModel.IdFrom(typeof(T)));

    string IModel.Id { get; } = Name;
}

