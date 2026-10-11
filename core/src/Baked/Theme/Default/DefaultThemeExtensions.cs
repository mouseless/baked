using Baked.Business;
using Baked.Domain;
using Baked.Domain.Model;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

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
                    return new MenuPage
                    {
                        Sections =
                        {
                            new()
                            {
                                Links =
                                [
                                    ..context.Sitemap
                                        .Where(smp => smp.SideMenu && !smp.Index)
                                        .Select(smp => smp.ToCardLink(l))
                                        .Select(l => new Filterable { Component = l })
                                ]
                            }
                        }
                    }.ToDescriptor();
                }

                var sections = context.Sitemap.GroupBy(smp => smp.Section);
                if (sections.Count() <= 1)
                {
                    return new MenuPage
                    {
                        Sections =
                        {
                            new()
                            {
                                Links =
                                [
                                    ..context.Sitemap
                                        .Where(r => r.ParentPath == context.Route.Path)
                                        .Select(r => r.ToCardLink(l))
                                        .Select(l => new Filterable { Component = l })
                                ]
                            }
                        },
                        Header = new PageTitle
                        {
                            LocalizeTitle = true,
                            Description = l(context.Route.Description)
                        }.ToDescriptor(data: Fetch.Inline(l(context.Route.Title)))
                    }.ToDescriptor();
                }

                return new MenuPage
                {
                    FilterEvent = "filter-changed",
                    Header = new PageTitle
                    {
                        LocalizeTitle = true,
                        Description = l(context.Route.Description),
                        Actions =
                        {
                            new Filter { Placeholder = l("Filter") }
                                .ToDescriptor(action: Execute.Publish.Event("filter-changed"))
                        }
                    }.ToDescriptor(data: Fetch.Inline(context.Route.Title)),
                    Sections =
                    [
                        ..sections
                            .Select(g => new MenuPage.Section
                            {
                                Title = l(g.Key),
                                Links =
                                [
                                    .. g.Where(r => r.ParentPath == context.Route.Path)
                                        .Select(r => new Filterable { Component = r.ToCardLink(l), Title = l(r.Title) })
                                ]
                            })
                            .Where(s => s.Links.Any())
                    ]
                }.ToDescriptor();
            };
    }

    extension(Route route)
    {
        public IComponentDescriptor ToCardLink(NewLocaleKey l) =>
            new ComponentDescriptor<CardLink>(new CardLink
            {
                Route = route.Path,
                Title = l(route.Title),
                Icon = route.Icon,
                Description = l(route.Description),
                Disabled = route.Disabled ? true : null,
                DisabledReason = l(route.DisabledReason)
            });

        public SideMenu.Item ToSideMenuItem(NewLocaleKey l) =>
            new()
            {
                Route = route.Path,
                Icon = route.Icon ?? throw new($"Icon is required for pages in side menu: `{route.Path}`"),
                Title = l(route.SideMenuTitle),
                Disabled = route.Disabled ? true : null
            };

        public Header.Item ToHeaderItem(NewLocaleKey l) =>
            new()
            {
                Route = route.Path,
                Title = l(route.HeaderTitle),
                Icon = route.Icon,
                ParentRoute = route.ParentPath
            };
    }

    extension(ModelCollection<PropertyModel> properties)
    {
        public IEnumerable<PropertyModel> GetDataProperties() =>
            properties
                .Having<UiData>()
                .Select(p => (property: p, data: p.Get<UiData>()))
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
                attribute: c => new UiRoute(routePath)
            );
        }

        public void SetMethodRoute<T>(string methodName, string routePath)
        {
            conventions.SetMethodAttribute(
                when: c => c.Type.Is<T>() && c.Method.Name == methodName,
                attribute: c => new UiRoute(routePath)
            );
        }
    }

    extension(Group group)
    {
        public string InputGroupKey { get => group[nameof(FormPage.InputGroup)]; set => group[nameof(FormPage.InputGroup)] = value; }
        public string SectionKey { get => group[nameof(FormPage.Section)]; set => group[nameof(FormPage.Section)] = value; }
        public string TabName { get => group[nameof(Tab)]; set => group[nameof(Tab)] = value; }
    }

    extension(ICustomAttributesModel model)
    {
        public string InputGroupKey =>
            model.Get<Group>().InputGroupKey;

        public string SectionKey =>
            model.Get<Group>().SectionKey;

        public string TabName =>
            model.Get<Group>().TabName.Kebaberize();
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