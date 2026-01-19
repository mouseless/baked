namespace Baked.Orm;

[AttributeUsage(AttributeTargets.Class)]
public class EntityAttribute(Type id, Type queryType)
    : Attribute
{
    public Type IdType { get; } = id;
    public Type QueryType { get; } = queryType;
}