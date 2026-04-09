using Baked.Business;

namespace Baked.Theme.Default;

[AttributeUsage(AttributeTargets.Method)]
public class ActionAttribute : Attribute, IMetadataSerializer
{
    public bool HideInLists { get; set; }
    public string? RoutePathBack { get; set; }

    IEnumerable<MetadataProperty> IMetadataSerializer.Properties =>
        [
            new(HideInLists),
            new(RoutePathBack)
        ];
}