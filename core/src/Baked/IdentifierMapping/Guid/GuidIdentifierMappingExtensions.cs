using Baked.IdentifierMapping;
using Baked.IdentifierMapping.Guid;

namespace Baked;

public static class GuidIdentifierMappingExtensions
{
    public static GuidIdentifierMappingFeature Guid(this IdentifierMappingConfigurator _) =>
        new();
}