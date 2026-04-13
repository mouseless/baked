namespace Baked.Business;

[AttributeUsage(AttributeTargets.Property)]
public class IdAttribute(string RouteName)
    : Attribute()
{
    public string RouteName { get; set; } = RouteName;
    public MappingOptions? Mapping { get; set; }

    public record MappingOptions(Type UserType)
    {
        public Type UserType { get; set; } = UserType;
        public Type? IdentifierGenerator { get; set; }
    }
}