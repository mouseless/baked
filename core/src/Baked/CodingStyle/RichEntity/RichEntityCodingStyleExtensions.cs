using Baked.CodingStyle.RichEntity;

namespace Baked;

public static class RichEntityCodingStyleExtensions
{
    extension(CodingStyleFeature.Configurator _)
    {
        public RichEntityCodingStyleFeature RichEntity() =>
            new();
    }
}