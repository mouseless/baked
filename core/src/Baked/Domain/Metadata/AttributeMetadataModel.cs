namespace Baked.Domain.Metadata;

public record AttributeMetadataModel(string Type)
{
    public AttributeMetadataModel(string type, params (string Property, object? Value)[] values)
        : this(type)
    {
        Values = values.ToDictionary(i => i.Property, i => i.Value);
    }

    public Dictionary<string, object?> Values { get; init; } = new();
}