namespace Do.Domain.Model;

public class TypeModel(Type type)
    : IModel
{
    readonly Type _type = type;

    public string Name { get; } = type.Name;
    public string? Namespace { get; } = type.Namespace;
    public bool IsAbstract { get; } = type.IsAbstract;
    public bool IsValueType { get; } = type.IsValueType;
    public bool IsStatic { get; } = type.IsAbstract && type.IsSealed;
    public string Id { get; } = IdFromType(type);
    public ModelCollection<ConstructorModel> Constructors { get; } = [];
    public ModelCollection<MethodModel> Methods { get; } = [];
    public ModelCollection<PropertyModel> Properties { get; } = [];
    public ModelCollection<TypeModel> GenericTypeArguments { get; } = [];
    public ModelCollection<TypeModel> CustomAttributes { get; } = [];
    public TypeModel? BaseType { get; internal set; }

    public bool IsSystemType => Namespace?.StartsWith("System") == true;

    public void Apply(Action<Type> action) => action(_type);

    public bool IsAssignableFrom<T>() => IsAssignableFrom(typeof(T));

    public bool IsAssignableFrom(Type type)
    {
        if (_type == type) { return true; }

        return BaseType?.IsAssignableFrom(type) == true;
    }

    public static string IdFromType(Type type) =>
        type.FullName ?? $"{type.Namespace}.{type.Name}[{string.Join(',', type.GenericTypeArguments.Select(IdFromType))}]";
}
