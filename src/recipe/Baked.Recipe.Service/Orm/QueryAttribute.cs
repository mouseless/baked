namespace Baked.Orm;

[AttributeUsage(AttributeTargets.Class)]
public class QueryAttribute(Type entityType)
    : Attribute
{
    public Type EntityType { get; } = entityType;
}