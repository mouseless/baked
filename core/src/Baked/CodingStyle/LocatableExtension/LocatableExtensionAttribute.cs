using Baked.Business;

namespace Baked.CodingStyle.LocatableExtension;

[AttributeUsage(AttributeTargets.Class)]
public class LocatableExtensionAttribute(Type locatableType)
    : Attribute, IMetadataSerializer
{
    public Type LocatableType { get; } = locatableType;

    IEnumerable<MetadataProperty> IMetadataSerializer.Properties =>
        [
            new(nameof(LocatableType), LocatableType),
        ];
}