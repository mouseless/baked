namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Struct)]
public class NamespaceAttribute(string value)
    : Attribute
{
    public string Value { get; } = value;
}