using Baked.Business;

namespace Baked.Theme;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class ContextBasedComponentAttribute(Type schemaType)
    : Attribute(), IComponentContextFilter, IMetadataSerializer
{
    public Type SchemaType { get; set; } = schemaType;
    public Func<ComponentContext, bool> Filter { get; set; } = _ => true;

    IEnumerable<MetadataProperty> IMetadataSerializer.Properties =>
        [
            new(SchemaType)
        ];

    bool IComponentContextFilter.AppliesTo(ComponentContext context) =>
        Filter(context);
}