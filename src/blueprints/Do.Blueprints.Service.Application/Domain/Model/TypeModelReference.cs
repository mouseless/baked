namespace Do.Domain.Model;

public class TypeModelReference(Type type, string id) : IModel
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
        action(type);

    public override string ToString() =>
        id;

    string IModel.Id => id;
}
