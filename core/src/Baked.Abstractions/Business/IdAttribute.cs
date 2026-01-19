namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class IdAttribute(Type Type, string Name, string RouteName) : Attribute
{
    public Type Type { get; set; } = Type;
    public string Name { get; set; } = Name;
    public string RouteName { get; set; } = RouteName;
}