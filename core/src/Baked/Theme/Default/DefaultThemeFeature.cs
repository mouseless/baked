using Baked.Architecture;
using Baked.RestApi;
using Baked.RestApi.Model;
using Baked.Runtime;
using Baked.Ui;
using Humanizer;

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
            // Property Defaults
            builder.Index.Property.Add<IdAttribute>();
            builder.Index.Property.Add<DataAttribute>();
            builder.Conventions.SetPropertyAttribute(
                attribute: () => new IdAttribute(),
                when: c => c.Property.Name == "Id"
            );
            builder.Conventions.SetPropertyAttribute(
                attribute: c => new DataAttribute(c.Property.Name.Camelize()) { Label = c.Property.Name.Titleize() },
                when: c => c.Property.IsPublic,
                order: -10
            );
            builder.Conventions.AddPropertyAttributeConfiguration<DataAttribute>(
                attribute: data => data.Visible = false,
                when: c => c.Property.Has<IdAttribute>()
            );
            builder.Conventions.AddPropertyComponent(
                component: () => B.None(),
                order: -10
            );
            builder.Conventions.AddPropertyComponent(
                component: () => B.Text(),
                when: c => c.Property.PropertyType.Is<string>() || c.Property.PropertyType.Is<Guid>()
            );

            // Method Defaults
            builder.Index.Method.Add<TabNameAttribute>();
            builder.Conventions.SetMethodAttribute(
                attribute: () => new TabNameAttribute(),
                when: c => c.Method.Has<ActionModelAttribute>(),
                order: RestApiLayer.MaxConventionOrder + 10
            );
            builder.Conventions.AddMethodSchema(
                schema: c => MethodRemote(c.Method),
                when: c => c.Method.Has<ActionModelAttribute>()
            );

            // Parameter Defaults
            builder.Conventions.AddParameterSchema(
                schema: (c, cc) => ParameterParameter(c.Parameter, cc),
                when: c => c.Parameter.Has<ParameterModelAttribute>()
            );
            builder.Conventions.AddParameterSchemaConfiguration<Parameter>(
                schema: (p, c) =>
                {
                    var api = c.Parameter.Get<ParameterModelAttribute>();

                    p.Required = !api.IsOptional ? true : null;
                    p.DefaultValue = api.DefaultValue;
                },
                when: c => c.Parameter.Has<ParameterModelAttribute>()
            );

            // Enum Data
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => EnumInline(c.Type, cc),
                when: c => c.Type.SkipNullable().IsEnum
            );

            // `DataTable` defaults
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                component: dt =>
                {
                    dt.Schema.Rows = 5;
                    dt.Schema.Paginator = true;
                }
            );
            builder.Conventions.AddMethodSchema(
                schema: (c, cc) => MethodDataTableExport(c.Method, cc),
                when: c => c.Method.Has<ComponentDescriptorBuilderAttribute<DataTable>>()
            );
            builder.Conventions.AddPropertySchema(
                schema: (c, cc) => PropertyDataTableColumn(c.Property, cc),
                when: c => c.Property.Has<DataAttribute>()
            );
            builder.Conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                schema: (dtc, c, cc) =>
                {
                    var (_, l) = cc;
                    var data = c.Property.Get<DataAttribute>();

                    dtc.Title = data.Label is not null ? l(data.Label) : null;
                    dtc.Exportable = true;
                }
            );
            builder.Conventions.AddPropertySchema(
                schema: (c, cc) => PropertyConditional(c.Property, cc),
                when: c => c.Property.Has<DataAttribute>()
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
                        ep.ErrorInfos[403] = B.ErrorPageInfo(
                            title: l("Access Denied"),
                            message: l("You do not have the permission to view the address or data specified."),
                            options: epi => epi.ShowSafeLinks = true
                        );
                        ep.ErrorInfos[404] = B.ErrorPageInfo(
                            title: l("Page Not Found"),
                            message: l("The page you want to view is either deleted or outdated."),
                            options: epi => epi.ShowSafeLinks = true
                        );
                        ep.ErrorInfos[500] = B.ErrorPageInfo(l("Unexpected Error"), l("Please contact system administrator."));
                        ep.ErrorInfos[502] = B.ErrorPageInfo(
                            l("Bad Gateway"),
                            l("The server received an invalid response from the upstream server. Please try again later."),
                            options: epi => epi.CustomMessage = true
                        );
                        ep.ErrorInfos[503] = B.ErrorPageInfo(
                            l("Service Unavailable"),
                            l("The service is currently unavailable. Please try again later."),
                            options: epi => epi.CustomMessage = true
                        );
                        ep.ErrorInfos[504] = B.ErrorPageInfo(
                            l("Gateway Timeout"),
                            l("The server did not receive a timely response from the upstream server. Please try again later."),
                            options: epi => epi.CustomMessage = true
                        );
                        ep.ErrorInfos[999] = B.ErrorPageInfo(l("Application Error"), l("Please contact system administrator."));

                        _errorPageOptions.Apply(ep);
                    },
                    data: Computed(Composables.UseError)
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
                    foreach (var route in _routes)
                    {
                        var page = route.BuildPage(new()
                        {
                            Route = route,
                            Sitemap = [.. _routes],
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