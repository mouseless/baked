namespace Do.Domain.Model;

public record PropertyModel(
    string Name,
    TypeModel PropertyType,
    bool IsPublic,
    bool IsVirtual
) : IModel
{
    public string Id => $"{PropertyType.Id}{Name}";
}
