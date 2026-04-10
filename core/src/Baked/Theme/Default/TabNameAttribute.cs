using Baked.Business;

namespace Baked.Theme.Default;

public class TabNameAttribute
    : Attribute, IMetadataSerializer
{
    public string Value { get; set; } = "Default";

    IEnumerable<MetadataProperty> IMetadataSerializer.Properties =>
        [
            new(Value)
        ];
}