using Baked.CodingStyle;
using Baked.CodingStyle.Unique;

namespace Baked;

public static class UniqueCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public UniqueCodingStyleFeature Unique() =>
            new();
    }
}