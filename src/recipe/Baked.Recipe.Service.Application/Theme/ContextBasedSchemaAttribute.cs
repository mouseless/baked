namespace Baked.Theme;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class ContextBasedSchemaAttribute(Type schemaType)
    : Attribute(), IComponentContextFilter
{
    public Type SchemaType { get; set; } = schemaType;
    public Func<ComponentContext, bool> Filter { get; set; } = _ => true;

    bool IComponentContextFilter.AppliesTo(ComponentContext context) =>
        Filter(context);
}