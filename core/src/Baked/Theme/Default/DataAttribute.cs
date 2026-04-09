using Baked.Business;

namespace Baked.Theme.Default;

[AttributeUsage(AttributeTargets.Property)]
public class DataAttribute(string prop)
    : Attribute(), IMetadataSerializer
{
    public string Prop { get; set; } = prop;
    public string? Label { get; set; }
    public bool Visible { get; set; } = true;
    public int Order { get; set; } = 0;

    IEnumerable<MetadataProperty> IMetadataSerializer.Properties =>
        [
            new(Prop),
            new(Label),
            new(Visible),
            new(Order)
        ];
}