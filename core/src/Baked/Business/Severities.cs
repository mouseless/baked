namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
public class Severities : Attribute
{
    public Dictionary<string, string> Value { get; } = [];
}