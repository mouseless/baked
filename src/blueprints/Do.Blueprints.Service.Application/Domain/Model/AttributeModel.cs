namespace Do.Domain.Model;

public record AttributeModel(
    TypeModel AttributeType
) : IModel
{
    public string Id { get; } = AttributeType.Id;
}
