namespace Do.Domain.Model;

public record ValueModel(
    TypeModel ValueType,
    object? Value
) : IModel
{
    public string Id { get; } = $"{Value ?? ValueType.Id}";
}
