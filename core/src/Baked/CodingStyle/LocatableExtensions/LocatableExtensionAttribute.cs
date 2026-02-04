namespace Baked.CodingStyle.LocatableExtensions;

[AttributeUsage(AttributeTargets.Class)]
public class LocatableExtensionAttribute(Type locatableType)
    : Attribute
{
    public Type LocatableType { get; } = locatableType;
}