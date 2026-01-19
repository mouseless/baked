namespace Baked.Orm;

[AttributeUsage(AttributeTargets.Class)]
public class EntityAttribute(Type queryType)
    : Attribute
{
    public Type QueryType { get; } = queryType;
}