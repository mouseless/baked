using Baked.CodingStyle;
using Baked.CodingStyle.SingleByUnique;

namespace Baked;

public static class SingleByUniqueCodingStyleExtensions
{
    public static SingleByUniqueCodingStyleFeature SingleByUnique(this CodingStyleConfigurator _) =>
        new();
}