namespace Baked.Theme;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class ContextBasedComponent(Type schemaType)
    : Attribute(), IComponentContextFilter
{
    public Type SchemaType { get; set; } = schemaType;
    public Func<ComponentContext, bool> FilterDelegate { get; set; } = _ => true;

    bool IComponentContextFilter.AppliesTo(ComponentContext context) =>
        FilterDelegate(context);
}