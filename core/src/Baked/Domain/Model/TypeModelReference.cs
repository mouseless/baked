using System.Collections.Concurrent;

namespace Baked.Domain.Model;

public class TypeModelReference(Type _type, string _id)
    : IModel
{
    static readonly ConcurrentDictionary<Type, string> _ids = new();

    internal static string IdFrom(Type type) =>
        _ids.GetOrAdd(type, static t =>
            t.IsGenericType ?
                $"{t.Namespace}.{t.Name}<{t.GenericTypeArguments.Select(IdFrom).Join(',')}>"
                : t.FullName ?? t.Name
        );

    internal static string IdOf(TypeModel typeDefinition, params IEnumerable<TypeModel> typeArguments) =>
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