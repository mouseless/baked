namespace Do.Domain.Model;

public interface IModel
{
    string Id { get; }

    static string IdFromType(Type type) => type.FullName ?? $"{type.Name},{type.Namespace}";
}
