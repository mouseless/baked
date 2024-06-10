using Baked.CodingStyle;
using Baked.CodingStyle.RichEntity;

namespace Baked;

public static class RichEntityCodingStyleExtensions
{
    public static RichEntityCodingStyleFeature RichEntity(this CodingStyleConfigurator _) =>
        new();
}