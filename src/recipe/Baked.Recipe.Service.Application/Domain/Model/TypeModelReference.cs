namespace Baked.Domain.Model;

public class TypeModelReference(Type _type, string _id)
    : IModel
{
    internal static string IdFrom(Type type) =>
        type.IsGenericType ?
            $"{type.Namespace}.{type.Name}<{type.GenericTypeArguments.Select(IdFrom).Join(',')}>"
            : type.FullName ?? type.Name;

    internal static string IdOf(TypeModel typeDefinition, params TypeModel[] typeArguments) =>
        $"{typeDefinition.Namespace}.{typeDefinition.Name}<{typeArguments.Select(t => t.CSharpFriendlyFullName).Join(',')}>";

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