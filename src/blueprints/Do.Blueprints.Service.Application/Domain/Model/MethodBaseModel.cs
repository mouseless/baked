namespace Do.Domain.Model;

public abstract record MethodBaseModel(
    bool IsPublic,
    bool IsProtected,
    bool IsVirtual,
    bool IsConstructor,
    TypeModel? ReturnType
) : IMemberModel
{
    public AttributeCollection CustomAttributes { get; private set; } = default!;
    public ModelCollection<ParameterModel> Parameters { get; private set; } = default!;

    internal void Init(AttributeCollection customAttributes, ModelCollection<ParameterModel> parameters)
    {
        CustomAttributes = customAttributes;
        Parameters = parameters;
    }

    public string Id => $"{ReturnType?.Name}[{string.Join(", ", Parameters.Select(p => p.Name))}]";
}