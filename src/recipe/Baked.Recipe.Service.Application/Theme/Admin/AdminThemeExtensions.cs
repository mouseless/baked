using Baked.Theme;
using Baked.Theme.Admin;
using Humanizer;

namespace Baked;

public static class AdminThemeExtensions
{
    public static AdminThemeFeature Admin(this ThemeConfigurator _,
        Func<string, string>? defaultPageName = default
    ) => new(defaultPageName ?? (s => s.Kebaberize()));
}