using Do.CodingStyle;
using Do.CodingStyle.CommandPattern;

namespace Do;

public static class CommandPatternCodingStyleExtensions
{
    public static CommandPatternCodingStyleFeature CommandPattern(this CodingStyleConfigurator _) =>
        new();
}