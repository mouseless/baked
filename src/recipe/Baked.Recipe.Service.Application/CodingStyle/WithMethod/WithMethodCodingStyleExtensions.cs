using Do.CodingStyle;
using Do.CodingStyle.WithMethod;

namespace Do;

public static class WithMethodCodingStyleExtensions
{
    public static WithMethodCodingStyleFeature WithMethod(this CodingStyleConfigurator _) =>
        new();
}