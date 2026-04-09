using Baked.Business;

namespace Baked.Authorization;

public class RequireUserAttribute(
  string[]? claims = default
) : Attribute, IMetadataSerializer
{
    public bool Override { get; set; }

    public string[] Claims => claims ?? [];

    IEnumerable<MetadataProperty> IMetadataSerializer.Properties =>
        [
            new(Override),
            new(Claims)
        ];
}