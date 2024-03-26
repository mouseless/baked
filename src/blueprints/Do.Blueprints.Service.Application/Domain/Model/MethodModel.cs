
namespace Do.Domain.Model;

// method group model
public record MethodModel(
    TypeModel Type,
    string Name,
    bool IsConstructor = false // constructor model olsun, method model'de is constructor olmayacak
) : IModel
{
    public OverloadModel[] Overloads { get; private set; } = default!; // methodmodel[], list olsun
    public AttributeCollection CustomAttributes { get; private set; } = default!; // kalkacak

    internal void Init(OverloadModel[] overloads, AttributeCollection customAttributes)
    {
        Overloads = overloads;
        CustomAttributes = customAttributes;
    }

    public List<ParameterModel> GetParameters() =>
        Overloads.SelectMany(o => o.Parameters).ToList();

    public bool HasParameter(Func<ParameterModel, bool> constraint) =>
        GetParameters().Any(p => constraint(p));

    public bool Has<T>() where T : Attribute =>
        CustomAttributes.ContainsKey(typeof(T));

    public bool CanReturn(TypeModel type) =>
        Overloads.Any(o =>
            o.ReturnType == type ||
            (o.ReturnType?.IsAssignableTo<Task>() == true && o.ReturnType?.GenericTypeArguments.Any(a => a == type) == true)
        );

    string IModel.Id { get; } = Name;
}
