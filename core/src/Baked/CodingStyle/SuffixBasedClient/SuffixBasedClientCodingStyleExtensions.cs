using Baked.CodingStyle;
using Baked.CodingStyle.SuffixBasedClient;

namespace Baked;

public static class SuffixBasedClientCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public SuffixBasedClientCodingStyleFeature SuffixBasedClient() =>
            new();
    }
}