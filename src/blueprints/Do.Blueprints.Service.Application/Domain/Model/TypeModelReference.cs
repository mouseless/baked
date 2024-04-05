namespace Do.Domain.Model;

public class TypeModelReference(Type _type, string _id)
    : IModel
{
    internal static string IdFrom(Type type) =>
        type.FullName ?? $"{type.Namespace}.{type.Name}<{string.Join(',', type.GenericTypeArguments.Select(IdFrom))}>";

    internal TypeModelReference(Type type) : this(type, IdFrom(type)) { }

    public TypeModel Model { get; private set; } = default!;

    internal void Init(TypeModel info)
    {
        Model = info;
    }

    public void Apply(Action<Type> action) =>
        action(_type);

    public override string ToString() =>
        _id;

    string IModel.Id => _id;
}
