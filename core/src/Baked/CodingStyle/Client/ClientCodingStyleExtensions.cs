using Baked.CodingStyle;
using Baked.CodingStyle.Client;

namespace Baked;

public static class ClientCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public ClientCodingStyleFeature Client() =>
            new();
    }
}