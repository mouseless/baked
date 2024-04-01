namespace Do.Domain.Model;

public abstract record MethodBaseModel(
    string Name,
    bool IsPublic,
    bool IsProtected,
    bool IsVirtual,
    bool IsConstructor,
    TypeModelReference? ReturnTypeReference,
    AttributeCollection CustomAttributes,
    ModelCollection<ParameterModel> Parameters
) : ICustomAttributesModel
{
    public TypeModel? ReturnType => ReturnTypeReference?.Model;
}