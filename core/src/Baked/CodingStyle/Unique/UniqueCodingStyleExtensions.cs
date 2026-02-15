using Baked.CodingStyle;
using Baked.CodingStyle.Unique;

namespace Baked;

public static class UniqueCodingStyleExtensions
{
    public static UniqueCodingStyleFeature Unique(this CodingStyleConfigurator _) =>
        new();
}