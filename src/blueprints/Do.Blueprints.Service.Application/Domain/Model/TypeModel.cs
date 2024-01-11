namespace Do.Domain.Model;

public record TypeModel(
    string Name,
    string Namespace,
    List<ConstructorModel> Constructors,
    List<MethodModel> Methods,
    List<PropertyModel> Properties,
    bool IsAbstract,
    bool IsValueType
)
{
    readonly Type _type = default!;

    public TypeModel(Type type)
        : this(type.Name, type.Namespace ?? string.Empty, [], [], [], type.IsAbstract, type.IsValueType)
    {
        _type = type;
    }

    internal void Apply(Action<Type> action) => action(_type);
}
