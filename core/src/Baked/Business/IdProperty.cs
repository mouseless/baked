using Baked.Binding;

namespace Baked.Business;

[AttributeUsage(AttributeTargets.Property)]
public class IdProperty(string RouteName)
    : Attribute(), IExportOptions
{
    public string RouteName { get; set; } = RouteName;
    public MappingOptions? Mapping { get; set; }
    string IExportOptions.Name => "Id";

    public record MappingOptions(Type UserType)
    {
        public Type UserType { get; set; } = UserType;
        public Type? IdentifierGenerator { get; set; }
    }
}