using Baked.Business;
using Baked.Domain;
using Baked.Domain.Model;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

using static Baked.Ui.Actions;

using B = Baked.Ui.Components;

namespace Baked;

public static class DefaultThemeExtensions
{
    extension(ThemeConfigurator _)
    {
        public DefaultThemeFeature Default(Func<Router, Route> index,
            IEnumerable<Func<Router, Route>>? routes = default,
            Action<ErrorPage>? errorPageOptions = default,
            Action<SideMenu>? sideMenuOptions = default,
            Action<Header>? headerOptions = default,
            ComponentPath.Debug? debugComponentPaths = default
        ) => new([index(new()), .. routes?.Select(r => r(new())) ?? []],
            _errorPageOptions: errorPageOptions,
            _sideMenuOptions: sideMenuOptions,
            _headerOptions: headerOptions,
            _debugComponentPaths: debugComponentPaths
        );
    }

    extension(Page.Describer _)
    {
        public PageBuilder Menu() =>
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
    }

    extension(Route route)
    {
        public IComponentDescriptor AsCardLink(NewLocaleKey l) =>
            B.CardLink(route.Path, l(route.Title), options: cl =>
            {
                cl.Icon = route.Icon;
                cl.Description = l(route.Description);
                cl.Disabled = route.Disabled ? true : null;
                cl.DisabledReason = l(route.DisabledReason);
            });

        public SideMenu.Item AsSideMenuItem(NewLocaleKey l) =>
            B.SideMenuItem(route.Path, route.Icon ?? throw new($"Icon is required for pages in side menu: `{route.Path}`"), options: smi =>
            {
                smi.Title = l(route.SideMenuTitle);
                smi.Disabled = route.Disabled ? true : null;
            });

        public Header.Item AsHeaderItem(NewLocaleKey l) =>
            B.HeaderItem(route.Path, options: hi =>
            {
                hi.Title = l(route.HeaderTitle);
                hi.Icon = route.Icon;
                hi.ParentRoute = route.ParentPath;
            });
    }

    extension(ModelCollection<PropertyModel> properties)
    {
        public IEnumerable<PropertyModel> GetDataProperties() =>
            properties
                .Having<DataAttribute>()
                .Select(p => (property: p, data: p.Get<DataAttribute>()))
                .Where(pd => pd.data.Visible)
                .OrderBy(pd => pd.data.Order)
                .Select(pd => pd.property);
    }

    extension(IDomainModelConventionCollection conventions)
    {
        public void SetTypeRoute<T>(string routePath)
        {
            conventions.SetTypeAttribute(
                when: c => c.Type.Is<T>(),
                attribute: c => new RouteAttribute(routePath)
            );
        }

        public void SetMethodRoute<T>(string methodName, string routePath)
        {
            conventions.SetMethodAttribute(
                when: c => c.Type.Is<T>() && c.Method.Name == methodName,
                attribute: c => new RouteAttribute(routePath)
            );
        }
    }

    extension(GroupAttribute group)
    {
        public string InputGroupKey { get => group[nameof(FormPage.InputGroup)]; set => group[nameof(FormPage.InputGroup)] = value; }
        public string SectionKey { get => group[nameof(FormPage.Section)]; set => group[nameof(FormPage.Section)] = value; }
        public string TabName { get => group[nameof(Tab)]; set => group[nameof(Tab)] = value; }
    }

    extension(ICustomAttributesModel model)
    {
        public string InputGroupKey =>
            model.Get<GroupAttribute>().InputGroupKey;

        public string SectionKey =>
            model.Get<GroupAttribute>().SectionKey;

        public string TabName =>
            model.Get<GroupAttribute>().TabName.Kebaberize();
    }

    extension<T>(IEnumerable<T> models) where T : ICustomAttributesModel
    {
        public IEnumerable<string> GetInputGroupKeys() =>
            models.Select(m => m.InputGroupKey).Distinct();

        public IEnumerable<string> GetSectionKeys() =>
            models.Select(m => m.SectionKey).Distinct();

        public IEnumerable<string> GetTabNames() =>
            models.Select(m => m.TabName).Distinct();
    }
}