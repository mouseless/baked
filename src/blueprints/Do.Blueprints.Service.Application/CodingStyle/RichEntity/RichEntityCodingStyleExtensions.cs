using Do.CodingStyle;
using Do.CodingStyle.RichEntity;

namespace Do;

public static class RichEntityCodingStyleExtensions
{
    public static RichEntityCodingStyleFeature RichEntity(this CodingStyleConfigurator _) =>
        new();
}