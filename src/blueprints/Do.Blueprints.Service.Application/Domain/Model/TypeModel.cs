namespace Do.Domain.Model;

public class TypeModel(Type type, string id)
    : IMemberModel, IModel, IEquatable<TypeModel>
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
    public bool IsGenericTypeDefinition { get; } = type.IsGenericTypeDefinition;
    public bool IsGenericTypeParameter { get; } = type.IsGenericTypeParameter;
    public bool IsGenericMethodParameter { get; } = type.IsGenericMethodParameter;
    public bool ContainsGenericParameters { get; } = type.ContainsGenericParameters;

    internal ModelCollection<MethodGroupModel> MethodGroups { get; private set; } = default!;
    public MethodModel[] Methods { get; private set; } = default!;
    public ModelCollection<PropertyModel> Properties { get; private set; } = default!;

    public AttributeCollection CustomAttributes { get; private set; } = default!;
    public ModelCollection<TypeModel> Interfaces { get; private set; } = default!;
    public TypeModel? BaseType { get; private set; }
    public TypeModel? GenericTypeDefinition { get; private set; }
    public ModelCollection<TypeModel> GenericTypeArguments { get; private set; } = default!;

    public string CSharpFriendlyFullName { get; private set; } = default!;

    public string RequiredFullName => throw new($"FullName was required for {Name}");
    internal ConstructorGroupModel? ConstructorGroup { get; private set; }
    public ConstructorModel[] Constructors { get; private set; } = default!;

    internal void SetGenerics(ModelCollection<TypeModel> genericTypeArguments,
        TypeModel? genericTypeDefinition = default
    )
    {
        GenericTypeArguments = genericTypeArguments;
        GenericTypeDefinition = genericTypeDefinition;

        CSharpFriendlyFullName = IsGenericType
            ? $"{Namespace}.{Name[..Name.IndexOf("`")]}<{string.Join(", ", GenericTypeArguments.Select(t => t.CSharpFriendlyFullName))}>"
            : FullName ?? Name;
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

    internal void SetMembers(ModelCollection<MethodGroupModel> methodGroups, ModelCollection<PropertyModel> properties,
        ConstructorGroupModel? constructor = default
    )
    {
        MethodGroups = methodGroups;
        Methods = MethodGroups.SelectMany(m => m.Methods).ToArray();
        Properties = properties;
        ConstructorGroup = constructor;
        Constructors = constructor?.Constructors.ToArray() ?? [];
    }

    public ConstructorModel? GetConstructor() => ConstructorGroup?.Constructors.Single();

    public MethodModel GetMethod(string name) => MethodGroups[name].Methods.Single();
    public MethodModel[] GetMethods(string name) => [.. MethodGroups[name].Methods];

    public void Apply(Action<Type> action) =>
        action(_type);

    public bool IsAssignableTo<T>() =>
        IsAssignableTo(typeof(T));

    public bool IsAssignableTo(Type type) =>
        Is(type) || Interfaces?.Contains(IdFrom(type)) == true;

    bool Is(Type type) =>
        _type == type || BaseType?.Is(type) == true || (type.IsGenericType && _type.IsGenericType && GenericTypeDefinition?.Is(type) == true);

    public bool Has<T>() where T : Attribute =>
        CustomAttributes.ContainsKey<T>();

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
