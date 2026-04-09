using Baked.Business;

namespace Baked.CodingStyle.EntitySubclass;

[AttributeUsage(AttributeTargets.Class)]
public class EntitySubclassAttribute(Type entityType, string name)
    : Attribute, IMetadataSerializer
{
    public Type EntityType { get; } = entityType;
    public string Name { get; } = name;

    IEnumerable<MetadataProperty> IMetadataSerializer.Properties =>
        [
            new(EntityType),
            new(Name),
        ];
}