using Baked.CodingStyle;
using Baked.CodingStyle.WithMethod;

namespace Baked;

public static class WithMethodCodingStyleExtensions
{
    public static WithMethodCodingStyleFeature WithMethod(this CodingStyleConfigurator _) =>
        new();
}