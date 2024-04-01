namespace Do.Domain.Model;

public record ParameterModel(
    string Name,
    TypeModelReference ParameterTypeReference,
    bool IsOptional,
    object? DefaultValue,
    AttributeCollection CustomAttributes
) : IModel, ICustomAttributesModel
{
    public TypeModel ParameterType => ParameterTypeReference.Model;

    public bool Has<T>() where T : Attribute =>
        CustomAttributes.ContainsKey<T>();

    string IModel.Id => Name;
}

