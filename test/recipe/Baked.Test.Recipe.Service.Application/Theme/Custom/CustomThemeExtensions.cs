using Baked.Theme;
using Baked.Theme.Admin;

namespace Baked;

public static class CustomThemeExtensions
{
    public static CustomThemeFeature Custom(this ThemeConfigurator _,
        IEnumerable<string>? componentExports = default
    ) => new([.. componentExports ?? []]);
}