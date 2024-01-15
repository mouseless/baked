namespace Do.Domain.Model;

public interface IModel
{
    string Id { get; }

    public static string IdFromType(Type type) =>
        type.FullName ?? $"{type.Namespace}.{type.Name}[{string.Join(',', type.GenericTypeArguments.Select(IdFromType))}]";
}
