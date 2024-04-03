using Do.Domain.Configuration;

namespace Do.Domain.Model;

public class TypeModel : IModel, IEquatable<TypeModel>
{
    readonly Lazy<string> _cSharpFriendlyFullName;

    protected TypeModel()
    {
        _cSharpFriendlyFullName = new(BuildCSharpFriendlyFullName);
    }

    Type Type { get; set; } = default!;
    string Id { get; set; } = default!;
    public string Name { get; private set; } = default!;
    public string? FullName { get; private set; } = default!;
    public string? Namespace { get; private set; } = default!;
    public bool IsPublic { get; private set; } = default!;
    public bool IsAbstract { get; private set; } = default!;
    public bool IsValueType { get; private set; } = default!;
    public bool IsSealed { get; private set; } = default!;
    public bool IsClass { get; private set; } = default!;
    public bool IsInterface { get; private set; } = default!;
    public bool IsArray { get; private set; } = default!;
    public bool IsGenericType { get; private set; } = default!;
    public bool IsGenericTypeDefinition { get; private set; } = default!;
    public bool IsGenericTypeParameter { get; private set; } = default!;
    public bool IsGenericMethodParameter { get; private set; } = default!;
    public bool ContainsGenericParameters { get; private set; } = default!;

    public string CSharpFriendlyFullName => _cSharpFriendlyFullName.Value;

    string BuildCSharpFriendlyFullName()
    {
        if (Type == typeof(void)) { return "void"; }
        if (!IsGenericType) { return FullName ?? Name; }

        if (this is TypeModelGenerics generics)
        {
            return $"{Namespace}.{Name[..Name.IndexOf("`")]}<{string.Join(", ", generics.GenericTypeArguments.Select(t => t.Model.CSharpFriendlyFullName))}>";
        }

        return BuildCSharpFriendlyFullName(Type);
    }

    static string BuildCSharpFriendlyFullName(Type type) =>
        $"{type.Namespace}.{type.Name[..type.Name.IndexOf("`")]}<{string.Join(", ", type.GenericTypeArguments.Select(BuildCSharpFriendlyFullName))}>";

    public void Apply(Action<Type> action) =>
        action(Type);

    public bool Is<T>() =>
        Is(typeof(T));

    public bool Is(Type type) =>
        Type == type;

    public bool IsAssignableTo<T>() =>
        IsAssignableTo(typeof(T));

    public bool IsAssignableTo(Type type) =>
        Is(type) ||
        this.TryGetGenerics(out var generics) && generics.GenericTypeDefinition?.IsAssignableTo(type) == true ||
        this.TryGetInheritance(out var inheritance) && (inheritance.BaseType?.IsAssignableTo(type) == true || inheritance.Interfaces.Contains(type))
    ;

    public override bool Equals(object? obj) =>
        ((IEquatable<TypeModel>)this).Equals(obj as TypeModel);

    bool IEquatable<TypeModel>.Equals(TypeModel? other) =>
        other is not null && other.Id == Id;

    public override int GetHashCode() =>
        Id.GetHashCode();

    string IModel.Id => Id;

    public class Factory
    {
        protected virtual TypeModel Create() => new();
        protected virtual void Fill(TypeModel result, Type type, DomainModelBuilder builder)
        {
            result.Type = type;
            result.Id = TypeModelReference.IdFrom(type);
            result.Name = type.Name;
            result.FullName = type.FullName;
            result.Namespace = type.Namespace;
            result.IsPublic = type.IsPublic;
            result.IsAbstract = type.IsAbstract;
            result.IsValueType = type.IsValueType;
            result.IsSealed = type.IsSealed;
            result.IsClass = type.IsClass;
            result.IsInterface = type.IsInterface;
            result.IsArray = type.IsArray;
            result.IsGenericType = type.IsGenericType;
            result.IsGenericTypeDefinition = type.IsGenericTypeDefinition;
            result.IsGenericTypeParameter = type.IsGenericTypeParameter;
            result.IsGenericMethodParameter = type.IsGenericMethodParameter;
            result.ContainsGenericParameters = type.ContainsGenericParameters;
        }

        public TypeModel Create(Type type, DomainModelBuilder builder)
        {
            var result = Create();

            Fill(result, type, builder);

            return result;
        }
    }
}
