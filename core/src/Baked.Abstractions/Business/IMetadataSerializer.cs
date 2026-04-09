namespace Baked.Business;

public interface IMetadataSerializer
{
    IEnumerable<MetadataProperty> Properties { get; }
}
