namespace Baked.CodingStyle.EntitySubclassViaComposition;

// TODO temporarily moved to here for build to succeed
// will be addressed later
[AttributeUsage(AttributeTargets.Method)]
public class SingleByUniqueAttribute(string propertyName, Type propertyType)
    : Attribute
{
    public string PropertyName { get; } = propertyName;
    public Type PropertyType { get; } = propertyType;
}