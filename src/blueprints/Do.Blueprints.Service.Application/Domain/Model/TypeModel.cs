namespace Do.Domain.Model;

public record TypeModel(
    string Name,
    string Namespace,
    ModelCollection<ConstructorModel> Constructors,
    ModelCollection<MethodModel> Methods,
    ModelCollection<PropertyModel> Properties,
    ModelCollection<TypeModel> GenericArguements,
    bool IsAbstract,
    bool IsValueType
) : IModel
{
    readonly Type _type = default!;

    public TypeModel(Type type)
        : this(type.Name, type.Namespace ?? string.Empty, [], [], [], [], type.IsAbstract, type.IsValueType)
    {
        _type = type;
    }

    internal void Apply(Action<Type> action) => action(_type);
}
