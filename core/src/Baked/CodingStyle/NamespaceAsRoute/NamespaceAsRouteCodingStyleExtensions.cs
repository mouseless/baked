using Baked.CodingStyle;
using Baked.CodingStyle.NamespaceAsRoute;

namespace Baked;

public static class NamespaceAsRouteCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public NamespaceAsRouteCodingStyleFeature NamespaceAsRoute() =>
            new();
    }
}