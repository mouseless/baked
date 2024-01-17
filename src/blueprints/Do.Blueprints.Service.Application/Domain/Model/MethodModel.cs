namespace Do.Domain.Model;

public record MethodModel(
    string Name,
    bool IsConstructor,
    ModelCollection<OverloadModel> Overloads
) : IModel
{
    public string Id { get; } = $"{Name}";
}
