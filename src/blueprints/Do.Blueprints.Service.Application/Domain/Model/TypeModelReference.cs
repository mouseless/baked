namespace Do.Domain.Model;

public class TypeModelReference : IKeyedModel
{
    internal static string IdFrom(Type type) =>
        type.FullName ?? $"{type.Namespace}.{type.Name}<{string.Join(',', type.GenericTypeArguments.Select(IdFrom))}>";

    readonly Type _type;
    readonly string _id;

    internal TypeModelReference(Type type) : this(type, IdFrom(type)) { }
    public TypeModelReference(Type type, string id)
    {
        _type = type;
        _id = id;
    }

    public TypeModel Model { get; private set; } = default!;

    internal void Init(TypeModel info)
    {
        Model = info;
    }

    public void Apply(Action<Type> action) =>
        action(_type);

    public override string ToString() =>
        _id;

    string IKeyedModel.Id => _id;
}
