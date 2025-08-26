using Baked.Theme;
using Baked.Theme.Admin;
using Baked.Ui;

using static Baked.Theme.Admin.Components;
using static Baked.Ui.UiLayer;

namespace Baked;

public static class AdminThemeExtensions
{
    public static AdminThemeFeature Admin(this ThemeConfigurator _, Page indexPage,
        IEnumerable<Page>? pages = default,
        Action<ErrorPage>? errorPageOptions = default,
        Action<SideMenu>? sideMenuOptions = default,
        Action<Header>? headerOptions = default
    ) => new([indexPage, .. pages ?? []],
        _errorPageOptions: errorPageOptions,
        _sideMenuOptions: sideMenuOptions,
        _headerOptions: headerOptions
    );

    public static IComponentDescriptor AsCardLink(this Page page, NewLocaleKey l) =>
        CardLink(page.Route, l(page.Title), options: cl =>
        {
            cl.Icon = page.Icon;
            cl.Description = l(page.Description);
            cl.Disabled = page.Disabled ? true : null;
            cl.DisabledReason = l(page.DisabledReason);
        });

    public static SideMenu.Item AsSideMenuItem(this Page page, NewLocaleKey l) =>
        SideMenuItem(page.Route, page.Icon ?? throw new($"Icon is required for pages in side menu: `{page.Route}`"), options: smi =>
        {
            smi.Title = l(page.SideMenuTitle);
            smi.Disabled = page.Disabled ? true : null;
        });

    public static Header.Item AsHeaderItem(this Page page, NewLocaleKey l) =>
        HeaderItem(page.Route, options: hi =>
        {
            hi.Title = l(page.HeaderTitle);
            hi.Icon = page.Icon;
            hi.ParentRoute = page.ParentRoute;
        });
}