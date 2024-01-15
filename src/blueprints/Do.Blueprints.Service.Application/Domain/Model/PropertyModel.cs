namespace Do.Domain.Model;

public record PropertyModel(
    string Name,
    TypeModel PropertyType,
    bool IsPublic
) : IModel
{
    public string Id => $"{PropertyType.Id}{Name}";
}
