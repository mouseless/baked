using Baked.Test.Theme.Custom;
using Baked.Theme;

namespace Baked;

public static class CustomThemeExtensions
{
    public static CustomThemeFeature Custom(this ThemeConfigurator _) =>
        new();
}