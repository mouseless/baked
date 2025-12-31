using Baked.Ux;
using Baked.Ux.RoutedTypesAsNavLinks;

namespace Baked;

public static class RoutedTypesAsNavLinksUxExtensions
{
    public static RoutedTypesAsNavLinksUxFeature RoutedTypesAsNavLinks(this UxConfigurator _) =>
        new();
}