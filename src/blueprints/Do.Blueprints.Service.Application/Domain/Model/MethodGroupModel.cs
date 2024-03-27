
namespace Do.Domain.Model;

// method group model
public record MethodGroupModel(
    TypeModel ReflectedType,
    string Name
) : IModel
{
    public List<MethodModel> Methods { get; private set; } = default!;
    public AttributeCollection CustomAttributes { get; private set; } = default!; // kalkacak

    internal void Init(List<MethodModel> methods, AttributeCollection customAttributes)
    {
        Methods = methods;
        CustomAttributes = customAttributes;
    }

    public bool Has<T>() where T : Attribute =>
        CustomAttributes.ContainsKey(typeof(T));

    string IModel.Id { get; } = Name;
}
