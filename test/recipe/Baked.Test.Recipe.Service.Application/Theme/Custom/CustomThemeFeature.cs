using Baked.Architecture;
using Baked.Theme.Admin;

using static Baked.Theme.Admin.Components;
using static Baked.Test.Theme.Custom.Components;
using static Baked.Ui.Datas;

namespace Baked.Test.Theme.Custom;

public class CustomThemeFeature(IEnumerable<Page> _pages)
    : AdminThemeFeature()
{

    public override void Configure(LayerConfigurator configurator)
    {
        base.Configure(configurator);

        configurator.ConfigureComponentExports(c =>
        {
            c.AddFromExtensions(typeof(Components));
        });

        configurator.ConfigureAppDescriptor(app =>
        {
            configurator.UsingLocalization(l =>
            {
                app.Error = ErrorPage(
                    options: ep =>
                    {
                        ep.SafeLinks.AddRange([.. _pages.Where(p => p.ErrorSafeLink).Select(p => p.AsCardLink(l))]);
                        ep.ErrorInfos[403] = ErrorPageInfo(l("Access Denied"), l("You do not have the permision to view the address or data specified."));
                        ep.ErrorInfos[404] = ErrorPageInfo(l("Page Not Found"), l("The page you want to view is etiher deleted or outdated."));
                        ep.ErrorInfos[500] = ErrorPageInfo(l("Unexpected Error"), l("Please contact system administrator."));
                    },
                    data: Computed(Composables.UseError)
                );
            });
        });

        configurator.ConfigureLayoutDescriptors(layouts =>
        {
            configurator.UsingLocalization(l =>
            {
                layouts.Add(DefaultLayout("default", options: dl =>
                {
                    dl.SideMenu = SideMenu(
                        options: sm =>
                        {
                            sm.Menu.AddRange([.. _pages.Where(p => p.SideMenu).Select(p => p.AsSideMenuItem(l))]);
                            sm.Footer = LanguageSwitcher();
                        }
                    );
                    dl.Header = Header(options: h =>
                    {
                        foreach (var page in _pages)
                        {
                            if (page.Disabled) { continue; }

                            h.Sitemap[page.Route] = page.AsHeaderItem(l);
                        }
                    });
                }));
            });

            layouts.Add(ModalLayout("modal"));
        });

        configurator.ConfigurePageDescriptors(pages =>
        {
            pages.Add(LoginPage("login", options: lp => lp.Layout = "modal"));
            pages.Add(RoutedPage("page/with/route/pageWithRoute", lp => lp.Layout = "default"));

            configurator.UsingDomainModel(domain =>
            {
                configurator.UsingLocalization(l =>
                {
                    foreach (var page in _pages)
                    {
                        if (page.Build is null) { continue; }

                        pages.Add(page.Build(new()
                        {
                            Page = page,
                            Sitemap = [.. _pages],
                            Domain = domain,
                            NewLocaleKey = l
                        }));
                    }
                });
            });
        });
    }
}