using Baked.Ux;
using Baked.Ux.InitializerParametersAreInPageTitle;

namespace Baked;

public static class InitializerParametersAreInPageTitleUxExtensions
{
    extension(UxConfigurator _)
    {
        public InitializerParametersAreInPageTitleUxFeature InitializerParametersAreInPageTitle() =>
            new();
    }
}