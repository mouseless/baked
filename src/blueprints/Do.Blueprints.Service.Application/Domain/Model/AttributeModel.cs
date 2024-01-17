namespace Do.Domain.Model;

public record AttributeModel(
    TypeModel AttributeType,
    ModelCollection<ValueModel> Values
) : IModel
{
    public string Id { get; } = AttributeType.Id;
}
