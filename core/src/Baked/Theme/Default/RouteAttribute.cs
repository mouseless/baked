using Baked.Business;

namespace Baked.Theme.Default;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RouteAttribute(string _path)
    : Attribute(), IMetadataSerializer
{
    public string Path { get; set; } = _path;
    public Dictionary<string, string> Params { get; init; } = [];

    IEnumerable<MetadataProperty> IMetadataSerializer.Properties =>
        [
            new(Path),
            new(Params)
        ];
}