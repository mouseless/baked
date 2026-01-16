namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class IdentifierAttribute(string Type, string Name, string RouteName) : Attribute
{
    public string Type { get; set; } = Type;
    public string Name { get; set; } = Name;
    public string RouteName { get; set; } = RouteName;
}