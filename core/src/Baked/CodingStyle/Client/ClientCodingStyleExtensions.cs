using Baked.CodingStyle;
using Baked.CodingStyle.Client;

namespace Baked;

public static class ClientCodingStyleExtensions
{
    public static ClientCodingStyleFeature Client(this CodingStyleConfigurator _) =>
        new();
}