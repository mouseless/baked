
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

    public bool Has<T>() where T : Attribute =>
        CustomAttributes.ContainsKey(typeof(T));

    string IModel.Id { get; } = Name;
}
