namespace Do.Domain.Model;

public class TypeModel(Type type, string id) : IModel, IEquatable<TypeModel>
{
    readonly Type _type = type;
    readonly string _id = id;

    public string Name { get; } = type.Name;
    public string? FullName { get; } = type.FullName;
    public string? Namespace { get; } = type.Namespace;
    public bool IsAbstract { get; } = type.IsAbstract;
    public bool IsValueType { get; } = type.IsValueType;
    public bool IsSealed { get; } = type.IsSealed;
    public bool IsInterface { get; } = type.IsInterface;
    public bool IsGenericTypeParameter { get; } = type.IsGenericTypeParameter;
    public bool IsGenericMethodParameter { get; } = type.IsGenericMethodParameter;
    public ModelCollection<MethodModel> Methods { get; private set; } = default!;
    public ModelCollection<PropertyModel> Properties { get; private set; } = default!;
    public ModelCollection<TypeModel> GenericTypeArguments { get; private set; } = default!;
    public ModelCollection<TypeModel> CustomAttributes { get; private set; } = default!;

    public MethodModel Constructor => Methods[".ctor"];

    internal void Init(
        ModelCollection<MethodModel> methods,
        ModelCollection<PropertyModel> properties,
        ModelCollection<TypeModel> genericTypeArguments,
        ModelCollection<TypeModel> customAttributes
    )
    {
        Methods = methods;
        Properties = properties;
        GenericTypeArguments = genericTypeArguments;
        CustomAttributes = customAttributes;
    }

    public void Apply(Action<Type> action) =>
        action(_type);

    public override bool Equals(object? obj) =>
        ((IEquatable<TypeModel>)this).Equals(obj as TypeModel);

    bool IEquatable<TypeModel>.Equals(TypeModel? other) =>
        other is not null && other._id == _id;

    public override int GetHashCode() =>
        _id.GetHashCode();

    string IModel.Id => _id;
}
