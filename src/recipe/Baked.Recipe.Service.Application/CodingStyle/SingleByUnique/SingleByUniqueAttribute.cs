namespace Baked.CodingStyle.SingleByUnique;

[AttributeUsage(AttributeTargets.Method)]
public class SingleByUniqueAttribute(string propertyName, Type propertyType)
    : Attribute
{
    public string PropertyName { get; } = propertyName;
    public Type PropertyType { get; } = propertyType;
}