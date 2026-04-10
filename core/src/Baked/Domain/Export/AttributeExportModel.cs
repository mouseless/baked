namespace Baked.Domain.Export;

public record AttributeExportModel(string Type)
{
    public AttributeExportModel(string type, params (string Property, object? Value)[] values)
        : this(type)
    {
        Values = values.ToDictionary(i => i.Property, i => i.Value);
    }

    public Dictionary<string, object?> Values { get; init; } = new();
}