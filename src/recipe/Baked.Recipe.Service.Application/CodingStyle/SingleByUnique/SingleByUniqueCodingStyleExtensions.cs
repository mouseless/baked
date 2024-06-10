using Do.CodingStyle;
using Do.CodingStyle.SingleByUnique;

namespace Do;

public static class SingleByUniqueCodingStyleExtensions
{
    public static SingleByUniqueCodingStyleFeature SingleByUnique(this CodingStyleConfigurator _) =>
        new();
}