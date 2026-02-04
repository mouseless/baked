namespace Baked.CodingStyle.EntitySubclass;

[AttributeUsage(AttributeTargets.Class)]
public class EntitySubclassAttribute(Type entityType, string name)
    : Attribute
{
    public Type EntityType { get; } = entityType;
    public string Name { get; } = name;
}