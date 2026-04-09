namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class)]
public class QueryAttribute(Type locatableType)
    : Attribute(), IMetadataSerializer
{
    public Type LocatableType { get; } = locatableType;

    IEnumerable<MetadataProperty> IMetadataSerializer.Properties =>
        [
            new(LocatableType)
        ];
}