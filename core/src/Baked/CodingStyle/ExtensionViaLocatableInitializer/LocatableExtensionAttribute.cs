namespace Baked.CodingStyle.ExtensionViaLocatableInitializer;

[AttributeUsage(AttributeTargets.Class)]
public class LocatableExtensionAttribute(Type locatableType)
    : Attribute
{
    public Type LocatableType { get; } = locatableType;
}