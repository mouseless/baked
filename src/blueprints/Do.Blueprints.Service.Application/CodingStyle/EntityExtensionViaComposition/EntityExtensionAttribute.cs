
namespace Do.CodingStyle.EntityExtensionViaComposition;

[AttributeUsage(AttributeTargets.Class)]
public class EntityExtensionAttribute(Type entityType)
    : Attribute
{
    public Type EntityType { get; } = entityType;
}