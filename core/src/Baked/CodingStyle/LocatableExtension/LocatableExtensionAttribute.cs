namespace Baked.CodingStyle.LocatableExtension;

[AttributeUsage(AttributeTargets.Class)]
public class LocatableExtensionAttribute(Type entityType)
    : Attribute
{
    public Type LocatableType { get; } = entityType;
}