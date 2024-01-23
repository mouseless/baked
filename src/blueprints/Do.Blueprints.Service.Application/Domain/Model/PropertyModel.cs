namespace Do.Domain.Model;

public record PropertyModel(
    string Name,
    TypeModel PropertyType,
    bool IsPublic,
    bool IsVirtual
) : IModel
{
    string IModel.Id { get; } = Name;
}
