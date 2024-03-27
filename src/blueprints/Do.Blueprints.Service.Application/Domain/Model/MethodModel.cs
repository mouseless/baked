namespace Do.Domain.Model;

public record MethodModel(
    MethodGroupModel Group,
    bool IsPublic,
    bool IsProtected,
    bool IsVirtual,
    TypeModel? ReturnType = default
)
{
    public ModelCollection<ParameterModel> Parameters { get; private set; } = default!;
    public AttributeCollection CustomAttributes { get; private set; } = default!;

    internal void Init(AttributeCollection customAttributes, ModelCollection<ParameterModel> parameters)
    {
        CustomAttributes = customAttributes;
        Parameters = parameters;
    }
}
