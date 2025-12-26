using Baked.Architecture;
using Baked.Business;
using Baked.RestApi;
using Baked.RestApi.Model;
using Baked.Runtime;
using Baked.Ui;
using Humanizer;
using System.Collections.Immutable;

using static Baked.Theme.Default.DomainComponents;
using static Baked.Theme.Default.DomainDatas;
using static Baked.Ui.Datas;

using B = Baked.Ui.Components;

namespace Baked.Theme.Default;

public class DefaultThemeFeature(IEnumerable<Route> _routes,
    Action<ErrorPage>? _errorPageOptions = default,
    Action<SideMenu>? _sideMenuOptions = default,
    Action<Header>? _headerOptions = default,
    Setting<bool>? _debugComponentPaths = default
) : IFeature<ThemeConfigurator>
{
    public virtual void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // Pages
            builder.Conventions.AddTypeComponent(
                where: cc => cc.Path.Is(nameof(Page), "*"),
                component: (c, cc) => TypeTabbedPage(c.Type, cc)
            );
            builder.Conventions.AddTypeComponent(
                where: cc => cc.Path.Is(nameof(Page), "*"),
                component: (c, cc) => TypeSimplePage(c.Type, cc)
            );
            builder.Conventions.AddMethodComponent(
                where: cc => cc.Path.Is(nameof(Page), "*", "*"),
                component: (c, cc) => MethodFormPage(c.Method, cc)
            );

            // `PageTitle` defaults
            builder.Conventions.AddTypeComponent(
                where: cc => cc.Path.Is(nameof(Page), "*", "*Page", "Title"),
                component: (c, cc) => TypePageTitle(c.Type, cc)
            );
            builder.Conventions.AddMethodComponent(
                where: cc => cc.Path.Is(nameof(Page), "*", "*", "*Page", "Title"),
                component: (c, cc) => MethodPageTitle(c.Method, cc)
            );
            builder.Conventions.AddTypeComponentConfiguration<PageTitle>(
                component: (pt, c, cc) =>
                {
                    foreach (var method in c.Type.GetMembers().Methods.Having<ActionAttribute>())
                    {
                        var action = method.GetAction();
                        if (action.Method == HttpMethod.Get) { continue; }
                        if (method.Has<InitializerAttribute>()) { continue; }

                        var actionComponent = method.GetComponent(cc.Drill(nameof(PageTitle.Actions), method.Name));
                        if (actionComponent is null) { continue; }

                        pt.Schema.Actions.Add(actionComponent);
                    }
                }
            );

            // Property defaults
            builder.Index.Property.Add<IdAttribute>();
            builder.Index.Property.Add<DataAttribute>();
            builder.Conventions.SetPropertyAttribute(
                when: c => c.Property.Name == "Id",
                attribute: () => new IdAttribute()
            );
            builder.Conventions.SetPropertyAttribute(
                when: c => c.Property.IsPublic,
                attribute: c => new DataAttribute(c.Property.Name.Camelize()) { Label = c.Property.Name.Titleize() },
                order: -10
            );
            builder.Conventions.AddPropertyAttributeConfiguration<DataAttribute>(
                when: c => c.Property.Has<IdAttribute>(),
                attribute: data => data.Visible = false
            );
            builder.Conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.Is<string>() || c.Property.PropertyType.Is<Guid>(),
                component: () => B.Text()
            );

            // Method defaults
            builder.Index.Method.Add<ActionAttribute>();
            builder.Index.Method.Add<TabNameAttribute>();
            builder.Conventions.SetMethodAttribute(
                when: c => c.Method.Has<ActionModelAttribute>(),
                attribute: () => new ActionAttribute(),
                order: RestApiLayer.MaxConventionOrder + 10
            );
            builder.Conventions.SetMethodAttribute(
                when: c => c.Method.Has<ActionModelAttribute>(),
                attribute: () => new TabNameAttribute(),
                order: RestApiLayer.MaxConventionOrder + 10
            );
            builder.Conventions.AddMethodSchema(
                schema: c => MethodRemote(c.Method)
            );
            builder.Conventions.AddMethodSchemaConfiguration<RemoteData>(
                when: c => c.Type.Has<LocatableAttribute>(),
                schema: rd => rd.Params = Computed.UseRoute("params")
            );
            builder.Conventions.AddMethodSchema(
                schema: (c, cc) => MethodContent(c.Method, cc)
            );
            builder.Conventions.AddMethodSchema(
                when: c => c.Method.Has<ActionAttribute>(),
                schema: (c, cc) => DomainActions.MethodRemote(c.Method)
            );
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Method.DefaultOverload.Parameters.Any(),
                schema: ra => ra.Body = Context.Model()
            );

            // Parameter defaults
            builder.Conventions.AddParameterSchema(
                when: c => c.Parameter.Has<ParameterModelAttribute>(),
                schema: (c, cc) => ParameterInput(c.Parameter, cc)
            );
            builder.Conventions.AddParameterSchemaConfiguration<Input>(
                when: c => c.Parameter.Has<ParameterModelAttribute>(),
                schema: (p, c) =>
                {
                    var api = c.Parameter.Get<ParameterModelAttribute>();

                    p.Required = !api.IsOptional ? true : null;
                    p.DefaultValue = api.DefaultValue;
                }
            );
            builder.Conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.Is<string>(),
                component: (c, cc) => ParameterInputText(c.Parameter, cc),
                order: UiLayer.MinConventionOrder + 10
            );
            builder.Conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<int>(),
                component: (c, cc) => ParameterInputNumber(c.Parameter, cc),
                order: UiLayer.MinConventionOrder + 10
            );

            // Enum Data
            builder.Conventions.AddTypeSchema(
                when: c => c.Type.SkipNullable().IsEnum,
                schema: (c, cc) => EnumInline(c.Type, cc)
            );
        });

        configurator.ConfigureComponentExports(exports =>
        {
            exports.AddFromExtensions(typeof(B));
        });

        configurator.ConfigureAppDescriptor(app =>
        {
            configurator.UsingLocalization(l =>
            {
                app.Error = B.ErrorPage(
                    options: ep =>
                    {
                        ep.SafeLinks.AddRange([.. _routes.Where(r => r.ErrorSafeLink).Select(r => r.AsCardLink(l))]);
                        ep.ErrorInfos[403] = B.ErrorPageInfo(l("Access Denied"), l("You do not have the permision to view the address or data specified."));
                        ep.ErrorInfos[404] = B.ErrorPageInfo(l("Page Not Found"), l("The page you want to view is either deleted or outdated."));
                        ep.ErrorInfos[500] = B.ErrorPageInfo(l("Unexpected Error"), l("Please contact system administrator."));
                        ep.ErrorInfos[999] = B.ErrorPageInfo(l("Application Error"), l("Please contact system administrator."));

                        _errorPageOptions.Apply(ep);
                    },
                    data: Computed.UseError()
                );
            });
        });

        configurator.ConfigureLayoutDescriptors(layouts =>
        {
            configurator.UsingLocalization(l =>
            {
                layouts.Add(B.DefaultLayout("default", options: dl =>
                {
                    dl.SideMenu = B.SideMenu(
                        options: sm =>
                        {
                            sm.Menu.AddRange([.. _routes.Where(r => r.SideMenu).Select(r => r.AsSideMenuItem(l))]);

                            _sideMenuOptions.Apply(sm);
                        }
                    );
                    dl.Header = B.Header(options: h =>
                    {
                        foreach (var route in _routes)
                        {
                            if (route.Disabled) { continue; }

                            h.Sitemap[route.Path] = route.AsHeaderItem(l);
                        }

                        _headerOptions.Apply(h);
                    });
                }));
            });

            layouts.Add(B.ModalLayout("modal"));
        });

        configurator.ConfigurePageDescriptors(pages =>
        {
            configurator.UsingDomainModel(domain =>
            {
                configurator.UsingLocalization(l =>
                {
                    var sitemap = _routes.ToImmutableList();
                    foreach (var route in _routes)
                    {
                        var page = route.BuildPage(new()
                        {
                            Route = route,
                            Sitemap = sitemap,
                            Domain = domain,
                            NewLocaleKey = l
                        });

                        if (page is null) { continue; }

                        pages.Add(page);
                    }
                });
            });

            if (_debugComponentPaths?.GetValue() == true)
            {
                Console.WriteLine("Component Paths:");
                Console.WriteLine("---");
                Console.WriteLine($"{ComponentPath.GetPaths().Join(Environment.NewLine)}");
            }
        });
    }
}