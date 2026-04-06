namespace Baked.Domain.Metadata;

public class TypeMetadataModel(string name)
{
    public string Name { get; set; } = name;
    public List<AttributeMetadataModel> Attributes { get; set; } = [];
    public List<PropertyMetadataModel> Properties { get; set; } = [];

    public record PropertyMetadataModel(string Name, List<AttributeMetadataModel> Attributes);
}