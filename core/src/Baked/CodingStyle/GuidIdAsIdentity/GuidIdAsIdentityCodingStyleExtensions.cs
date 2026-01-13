using Baked.CodingStyle;
using Baked.CodingStyle.GuidIdAsIdentity;

namespace Baked;

public static class GuidIdAsIdentityCodingStyleExtensions
{
    public static GuidIdAsIdentityCodingStyleFeature GuidIdAsIdentity(this CodingStyleConfigurator _) =>
        new();
}