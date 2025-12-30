using Baked.Domain;
using Baked.Domain.Model;
using Baked.Runtime;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;

using static Baked.Ui.Actions;

using B = Baked.Ui.Components;

namespace Baked;

public static class DefaultThemeExtensions
{
    public static DefaultThemeFeature Default(this ThemeConfigurator _, Func<Router, Route> index,
        IEnumerable<Func<Router, Route>>? routes = default,
        Action<ErrorPage>? errorPageOptions = default,
        Action<SideMenu>? sideMenuOptions = default,
        Action<Header>? headerOptions = default,
        Setting<bool>? debugComponentPaths = default
    ) => new([index(new()), .. routes?.Select(r => r(new())) ?? []],
        _errorPageOptions: errorPageOptions,
        _sideMenuOptions: sideMenuOptions,
        _headerOptions: headerOptions,
        _debugComponentPaths: debugComponentPaths
    );

    public static PageBuilder Menu(this Page.Describer _) =>
        context =>
        {
            var (_, l) = context;

            if (context.Route.Index)
            {
                return B.MenuPage(context.Route.Name,
                    links: context.Sitemap
                        .Where(smp => smp.SideMenu && !smp.Index)
                        .Select(smp => smp.AsCardLink(l))
                );
            }

            var sections = context.Sitemap.GroupBy(smp => smp.Section);
            if (sections.Count() <= 1)
            {
                return B.MenuPage(context.Route.Name,
                    links: context.Sitemap
                        .Where(r => r.ParentPath == context.Route.Path)
                        .Select(r => r.AsCardLink(l)),
                    options: mp =>
                    {
                        mp.Header = B.PageTitle(
                            title: l(context.Route.Title),
                            options: pt => pt.Description = l(context.Route.Description)
                        );
                    }
                );
            }

            return B.MenuPage(context.Route.Name,
                options: mp =>
                {
                    mp.Header = B.PageTitle(context.Route.Title, options: pt =>
                    {
                        pt.Description = l(context.Route.Description);
                        pt.Actions.Add(B.Filter(
                            options: f => f.Placeholder = l("Filter"),
                            action: Publish.Event("filter-changed")
                        ));
                    });
                    mp.FilterEvent = "filter-changed";
                    mp.Sections.AddRange(
                        sections.Select(g => B.MenuPageSection(
                            options: mps =>
                            {
                                mps.Title = l(g.Key);
                                mps.Links.AddRange(g
                                    .Where(r => r.ParentPath == context.Route.Path)
                                    .Select(r => B.Filterable(r.AsCardLink(l), options: f => f.Title = l(r.Title)))
                                );
                            }
                        )).Where(s => s.Links.Any())
                    );
                }
            );
        };

    public static IComponentDescriptor AsCardLink(this Route route, NewLocaleKey l) =>
        B.CardLink(route.Path, l(route.Title), options: cl =>
        {
            cl.Icon = route.Icon;
            cl.Description = l(route.Description);
            cl.Disabled = route.Disabled ? true : null;
            cl.DisabledReason = l(route.DisabledReason);
        });

    public static SideMenu.Item AsSideMenuItem(this Route route, NewLocaleKey l) =>
        B.SideMenuItem(route.Path, route.Icon ?? throw new($"Icon is required for pages in side menu: `{route.Path}`"), options: smi =>
        {
            smi.Title = l(route.SideMenuTitle);
            smi.Disabled = route.Disabled ? true : null;
        });

    public static Header.Item AsHeaderItem(this Route route, NewLocaleKey l) =>
        B.HeaderItem(route.Path, options: hi =>
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

    public static void SetTypeRoute<T>(this IDomainModelConventionCollection conventions, string routePath)
    {
        conventions.SetTypeAttribute(
            when: c => c.Type.Is<T>(),
            attribute: c => new RouteAttribute(routePath)
        );
    }

    public static void SetMethodRoute<T>(this IDomainModelConventionCollection conventions, string methodName, string routePath)
    {
        conventions.SetMethodAttribute(
            when: c => c.Type.Is<T>() && c.Method.Name == methodName,
            attribute: c => new RouteAttribute(routePath)
        );
    }
}