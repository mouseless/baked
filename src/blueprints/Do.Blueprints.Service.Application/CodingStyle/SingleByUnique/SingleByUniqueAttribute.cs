namespace Do.CodingStyle.SingleByUnique;

[AttributeUsage(AttributeTargets.Method)]
public class SingleByUniqueAttribute(string propertyName)
    : Attribute
{
    public string PropertyName { get; } = propertyName;
}