using Baked.Domain.Model;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;

using static Baked.Theme.Default.Components;
using static Baked.Ui.UiLayer;

namespace Baked;

public static class DefaultThemeExtensions
{
    public static DefaultThemeFeature Default(this ThemeConfigurator _, Func<Router, Route> index,
        IEnumerable<Func<Router, Route>>? routes = default,
        Action<ErrorPage>? errorPageOptions = default,
        Action<SideMenu>? sideMenuOptions = default,
        Action<Header>? headerOptions = default
    ) => new([index(new()), .. routes?.Select(r => r(new())) ?? []],
        _errorPageOptions: errorPageOptions,
        _sideMenuOptions: sideMenuOptions,
        _headerOptions: headerOptions
    );

    public static PageBuilder Menu(this Page.Describer _) =>
        context =>
        {
            var (_, l) = context;

            if (context.Route.Index)
            {
                return MenuPage(context.Route.Name,
                    links: context.Sitemap
                        .Where(smp => !smp.Index && smp.SideMenu)
                        .Select(smp => smp.AsCardLink(l))
                );
            }

            var sections = context.Sitemap.GroupBy(smp => smp.Section);
            if (sections.Count() <= 1)
            {
                return MenuPage(context.Route.Name,
                    links: context.Sitemap
                        .Where(r => r.ParentPath == context.Route.Path)
                        .Select(r => r.AsCardLink(l)),
                    options: mp =>
                    {
                        mp.Header = PageTitle(
                            title: l(context.Route.Title),
                            options: pt => pt.Description = l(context.Route.Description)
                        );
                    }
                );
            }

            return MenuPage(context.Route.Name,
                options: mp =>
                {
                    mp.Header = PageTitle(context.Route.Title, options: pt =>
                    {
                        pt.Description = l(context.Route.Description);
                        pt.Actions.Add(Filter("menu-page", options: f => f.Placeholder = l("Filter")));
                    });
                    mp.FilterPageContextKey = "menu-page";
                    mp.Sections.AddRange(
                        sections.Select(g => MenuPageSection(
                            options: mps =>
                            {
                                mps.Title = l(g.Key);
                                mps.Links.AddRange(g
                                    .Where(r => r.ParentPath == context.Route.Path)
                                    .Select(r => Filterable(r.AsCardLink(l), options: f => f.Title = l(r.Title)))
                                );
                            }
                        )).Where(s => s.Links.Any())
                    );
                }
            );
        };

    public static IComponentDescriptor AsCardLink(this Route route, NewLocaleKey l) =>
        CardLink(route.Path, l(route.Title), options: cl =>
        {
            cl.Icon = route.Icon;
            cl.Description = l(route.Description);
            cl.Disabled = route.Disabled ? true : null;
            cl.DisabledReason = l(route.DisabledReason);
        });

    public static SideMenu.Item AsSideMenuItem(this Route route, NewLocaleKey l) =>
        SideMenuItem(route.Path, route.Icon ?? throw new($"Icon is required for pages in side menu: `{route.Path}`"), options: smi =>
        {
            smi.Title = l(route.SideMenuTitle);
            smi.Disabled = route.Disabled ? true : null;
        });

    public static Header.Item AsHeaderItem(this Route route, NewLocaleKey l) =>
        HeaderItem(route.Path, options: hi =>
        {
            hi.Title = l(route.HeaderTitle);
            hi.Icon = route.Icon;
            hi.ParentRoute = route.ParentPath;
        });

    public static IEnumerable<PropertyModel> GetDataProperties(this ModelCollection<PropertyModel> properties) =>
        properties
            .Having<DataAttribute>()
            .Select(p => (property: p, data: p.Get<DataAttribute>()))
            .Where(pd => pd.data.Visible)
            .OrderBy(pd => pd.data.Order)
            .Select(pd => pd.property);
}