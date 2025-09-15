using Baked.Ux;
using Baked.Ux.InitializerParameterIsInPageTitle;

namespace Baked;

public static class InitializerParameterIsInPageTitleUxExtensions
{
    public static InitializerParameterIsInPageTitleUxFeature InitializerParameterIsInPageTitle(this UxConfigurator _) =>
        new();
}