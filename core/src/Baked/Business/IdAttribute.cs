namespace Baked.Business;

[AttributeUsage(AttributeTargets.Property)]
public class IdAttribute(string RouteName)
    : Attribute(), IMetadataSerializer
{
    public string RouteName { get; set; } = RouteName;
    public MappingOptions? Mapping { get; set; }

    IEnumerable<MetadataProperty> IMetadataSerializer.Properties =>
        [
            new(RouteName),
            new(Mapping),
        ];

    public record MappingOptions(Type UserType)
    {
        public Type UserType { get; set; } = UserType;
        public Type? IdentifierGenerator { get; set; }
    }
}