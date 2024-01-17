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
    public bool IsInterface { get; } = type.IsInterface;
    public bool IsGenericTypeParameter { get; } = type.IsGenericTypeParameter;
    public bool IsGenericMethodParameter { get; } = type.IsGenericMethodParameter;
    public string Id { get; } = IdFromType(type);
    public ModelCollection<MethodModel> Methods { get; } = [];
    public ModelCollection<PropertyModel> Properties { get; } = [];
    public ModelCollection<TypeModel> GenericTypeArguments { get; } = [];
    public ModelCollection<AttributeModel> CustomAttributes { get; } = [];
    public MethodModel Constructor => Methods[".ctor"];

    public bool IsSystemType => Namespace?.StartsWith("System") == true;

    public void Apply(Action<Type> action) => action(_type);

    public bool IsAssignableTo<T>() => IsAssignableTo(typeof(T));
    public bool IsAssignableTo(Type type) => _type.IsAssignableTo(type);

    public static string IdFromType(Type type) =>
        type.FullName ?? $"{type.Namespace}.{type.Name}[{string.Join(',', type.GenericTypeArguments.Select(IdFromType))}]";
}
