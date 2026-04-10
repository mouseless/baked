namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class NamespaceAttribute(string value)
    : Attribute(), IMetadataSerializer
{
    public string Value { get; } = value;

    IEnumerable<MetadataProperty> IMetadataSerializer.Properties =>
        [
            new(Value)
        ];
}