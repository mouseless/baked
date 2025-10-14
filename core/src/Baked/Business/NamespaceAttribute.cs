namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class NamespaceAttribute(string value)
    : Attribute
{
    public string Value { get; } = value;
}