using Baked.Ux;
using Baked.Ux.InitializerParametersAreInPageTitle;

namespace Baked;

public static class InitializerParametersAreInPageTitleUxExtensions
{
    public static InitializerParametersAreInPageTitleUxFeature InitializerParametersAreInPageTitle(this UxConfigurator _) =>
        new();
}