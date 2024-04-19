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

    string IModel.Id => Name;
}