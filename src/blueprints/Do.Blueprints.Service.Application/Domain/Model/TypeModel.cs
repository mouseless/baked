namespace Do.Domain.Model;

public class TypeModel(Type type, string id)
    : IModel, IEquatable<TypeModel>
{
    internal static string IdFrom(Type type) =>
        type.FullName ?? $"{type.Namespace}.{type.Name}<{string.Join(',', type.GenericTypeArguments.Select(IdFrom))}>";

    readonly Type _type = type;
    readonly string _id = id;

    internal TypeModel(Type type)
        : this(type, IdFrom(type)) { }

    public string Name { get; } = type.Name;
    public string? FullName { get; } = type.FullName;
    public string? Namespace { get; } = type.Namespace;
    public bool IsPublic { get; } = type.IsPublic;
    public bool IsAbstract { get; } = type.IsAbstract;
    public bool IsValueType { get; } = type.IsValueType;
    public bool IsSealed { get; } = type.IsSealed;
    public bool IsInterface { get; } = type.IsInterface;
    public bool IsGenericType { get; } = type.IsGenericType;
    public bool IsGenericTypeParameter { get; } = type.IsGenericTypeParameter;
    public bool IsGenericMethodParameter { get; } = type.IsGenericMethodParameter;
    public bool ContainsGenericParameters { get; } = type.ContainsGenericParameters;
    public TypeModel? BaseType { get; private set; } = default!;
    public ModelCollection<MethodModel> Methods { get; private set; } = default!;
    public ModelCollection<PropertyModel> Properties { get; private set; } = default!;
    public ModelCollection<TypeModel> GenericTypeArguments { get; private set; } = default!;
    public AttributeCollection CustomAttributes { get; private set; } = default!;
    public ModelCollection<TypeModel> Interfaces { get; private set; } = default!;
    public MethodModel? Constructor { get; private set; }

    void Init(
        ModelCollection<TypeModel>? genericTypeArguments = default,
        MethodModel? constructor = default,
        ModelCollection<MethodModel>? methods = default,
        ModelCollection<PropertyModel>? properties = default,
        AttributeCollection? customAttributes = default,
        ModelCollection<TypeModel>? interfaces = default,
        TypeModel? baseType = default
    )
    {
        GenericTypeArguments ??= genericTypeArguments ?? [];
        Constructor ??= constructor;
        Methods ??= methods ?? [];
        Properties = properties ?? [];
        CustomAttributes = customAttributes ?? [];
        Interfaces = interfaces ?? [];
        BaseType = baseType;
    }

    internal void SetGenerics(ModelCollection<TypeModel> genericTypeArguments)
    {
        GenericTypeArguments = genericTypeArguments;
    }

    internal void SetInheritance(ModelCollection<TypeModel> interfaces,
        TypeModel? baseType = default
    )
    {
        BaseType = baseType;
        Interfaces = interfaces;
    }

    internal void SetMetadata(AttributeCollection customAttributes)
    {
        CustomAttributes = customAttributes;
    }

    internal void SetMembers(ModelCollection<MethodModel> methods, ModelCollection<PropertyModel> properties,
        MethodModel? constructor = default
    )
    {
        Methods = methods;
        Properties = properties;
        Constructor = constructor;
    }

    public OverloadModel GetMethod(string name) => Methods[name].Overloads.Single();
    public OverloadModel[] GetMethods(string name) => Methods[name].Overloads;
    public OverloadModel[] GetMethods() => Methods.SelectMany(m => m.Overloads).ToArray(); // tabi bu array on demand olmasin, prebuilt olsun

    public void Apply(Action<Type> action) =>
        action(_type);

    public bool IsAssignableTo<T>() =>
        IsAssignableTo(typeof(T));

    public bool IsAssignableTo(Type type) =>
        Is(type) || Interfaces.Contains(IdFrom(type));

    bool Is(Type type) =>
        _type == type || BaseType?.Is(type) == true;

    public bool Has<T>() where T : Attribute =>
        CustomAttributes.ContainsKey(typeof(T));

    public override bool Equals(object? obj) =>
        ((IEquatable<TypeModel>)this).Equals(obj as TypeModel);

    bool IEquatable<TypeModel>.Equals(TypeModel? other) =>
        other is not null && other._id == _id;

    public override int GetHashCode() =>
        _id.GetHashCode();

    string IModel.Id => _id;
}

public static class TypeModelExtensions
{
    public static void Apply(this IEnumerable<TypeModel> types, Action<Type> action)
    {
        foreach (var type in types)
        {
            type.Apply(i => action(i));
        }
    }

    public static bool Contains(this ModelCollection<TypeModel> source, Type type) =>
        source.Contains(TypeModel.IdFrom(type));
}
