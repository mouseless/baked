namespace Baked.Domain.Metadata;

public class TypeMetadataModel
{
    public string Name { get; set; } = default!;
    public List<AttributeMetadataModel> Attributes { get; set; } = [];
    public List<PropertyMetadataModel> Properties { get; set; } = [];

    public record PropertyMetadataModel(string Name, List<AttributeMetadataModel> Attributes);
}