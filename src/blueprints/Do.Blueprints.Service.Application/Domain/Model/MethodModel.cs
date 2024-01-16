namespace Do.Domain.Model;

public record MethodModel(
    string Name,
    TypeModel ReturnType,
    bool IsPublic,
    bool IsProtected,
    bool IsVirtual,
    ModelCollection<OverloadModel> Overloads
) : IModel
{
    public string Id { get; } = $"{Name}";
}
