namespace Baked.CodingStyle.ExtensionViaLocatableInitializer;

[AttributeUsage(AttributeTargets.Class)]
public class LocatableExtension(Type locatableType)
    : Attribute
{
    public Type LocatableType { get; } = locatableType;
}