using Baked.CodingStyle;
using Baked.CodingStyle.RichEntity;

namespace Baked;

public static class RichEntityCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public RichEntityCodingStyleFeature RichEntity() =>
            new();
    }
}