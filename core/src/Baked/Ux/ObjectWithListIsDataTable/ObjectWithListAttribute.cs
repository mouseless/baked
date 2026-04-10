using Baked.Business;

namespace Baked.Ux.ObjectWithListIsDataTable;

[AttributeUsage(AttributeTargets.Class)]
public class ObjectWithListAttribute(string listPropertyName)
    : Attribute(), IMetadataSerializer
{
    public string ListPropertyName { get; set; } = listPropertyName;

    IEnumerable<MetadataProperty> IMetadataSerializer.Properties =>
        [
            new(ListPropertyName)
        ];
}