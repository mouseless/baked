using Baked.CodingStyle;
using Baked.CodingStyle.NamespaceAsRoute;

namespace Baked;

public static class NamespaceAsRouteCodingStyleExtensions
{
    public static NamespaceAsRouteCodingStyleFeature NamespaceAsRoute(this CodingStyleConfigurator _) =>
        new();
}