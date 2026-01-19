using Baked.Business;

namespace Baked.Orm;

[AttributeUsage(AttributeTargets.Class)]
public class EntityAttribute(IdAttribute id, Type queryType)
    : Attribute
{
    public IdAttribute Id { get; } = id;
    public Type QueryType { get; } = queryType;
}