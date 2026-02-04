using Baked.CodingStyle;
using Baked.CodingStyle.Initializable;

namespace Baked;

public static class InitializableCodingStyleExtensions
{
    public static InitializableCodingStyleFeature Initializable(this CodingStyleConfigurator _) =>
        new();
}