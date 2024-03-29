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
) : IMemberModel
{
    public TypeModel? ReturnType => ReturnTypeReference?.Model;

    string IModel.Id { get; } = $"{ReturnTypeReference} {Name}({string.Join(", ", Parameters.Select(p => p.Name))})";
}