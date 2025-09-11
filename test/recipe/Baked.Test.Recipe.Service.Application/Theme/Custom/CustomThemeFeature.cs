using Baked.Architecture;
using Baked.Theme;
using Baked.Theme.Admin;

using static Baked.Theme.Admin.Components;
using static Baked.Test.Theme.Custom.Components;

namespace Baked.Test.Theme.Custom;

public class CustomThemeFeature(IEnumerable<Func<Router, Baked.Theme.Route>> _routes)
    : AdminThemeFeature(_routes.Select(r => r(new())),
        _sideMenuOptions: sm => sm.Footer = LanguageSwitcher()
    )
{
    public override void Configure(LayerConfigurator configurator)
    {
        base.Configure(configurator);

        configurator.ConfigureComponentExports(c =>
        {
            c.AddFromExtensions(typeof(Components));
        });

        configurator.ConfigurePageDescriptors(pages =>
        {
            pages.Add(LoginPage("login", options: lp => lp.Layout = "modal"));
            pages.Add(RoutedPage("page/with/route/pageWithRoute", lp => lp.Layout = "default"));
        });
    }
}