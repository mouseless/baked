namespace Do.Domain.Model;

public record PropertyModel(
    string Name,
    TypeModel Type,
    bool IsPublic
) : IModel
{
    public string Id => Name;
}
