
namespace Do.Domain.Model;

public record MethodModel(
    TypeModel Type,
    string Name,
    bool IsConstructor = false
) : IModel
{
    public OverloadModel[] Overloads { get; private set; } = default!;
    public AttributeCollection CustomAttributes { get; private set; } = default!;

    internal void Init(OverloadModel[] overloads, AttributeCollection customAttributes)
    {
        Overloads = overloads;
        CustomAttributes = customAttributes;
    }

    public List<ParameterModel> GetParameters() =>
        Overloads.SelectMany(o => o.Parameters).ToList();

    public bool HasParameter(Func<ParameterModel, bool> constraint) =>
        GetParameters().Any(p => constraint(p));

    public bool HasAttribute<T>() where T : Attribute =>
        CustomAttributes.ContainsKey(typeof(T));

    public bool CanReturn(TypeModel type) =>
        Overloads.Any(o =>
            o.ReturnType == type ||
            (o.ReturnType?.IsAssignableTo<Task>() == true && o.ReturnType?.GenericTypeArguments.Any(a => a == type) == true)
        );

    string IModel.Id { get; } = Name;
}
