using Baked.Ux;
using Baked.Ux.RoutedTypesAsNavLinks;

namespace Baked;

public static class RoutedTypesAsNavLinksUxExtensions
{
    extension(UxConfigurator _)
    {
        public RoutedTypesAsNavLinksUxFeature RoutedTypesAsNavLinks() =>
            new();
    }
}