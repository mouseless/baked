using Baked.Architecture;
using Baked.RestApi.Model;
using Humanizer;

using static Baked.Theme.Default.Components;
using static Baked.Theme.Default.DomainComponents;
using static Baked.Theme.Default.DomainDatas;
using static Baked.Ui.Datas;

namespace Baked.Theme.Default;

public class DefaultThemeFeature(IEnumerable<Route> _routes,
    Action<ErrorPage>? _errorPageOptions = default,
    Action<SideMenu>? _sideMenuOptions = default,
    Action<Header>? _headerOptions = default
) : IFeature<ThemeConfigurator>
{
    public virtual void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // Property Defaults
            builder.Index.Property.Add<DataAttribute>();
            builder.Conventions.SetPropertyMetadata(
                attribute: c => new DataAttribute(c.Property.Name.Camelize()) { Label = c.Property.Name.Titleize() },
                when: c => c.Property.IsPublic,
                order: -10
            );
            builder.Conventions.AddPropertyComponent(
                component: () => None(),
                order: -10
            );
            builder.Conventions.AddPropertyComponent(
                component: () => String(),
                whenProperty: c => c.Property.PropertyType.Is<string>() || c.Property.PropertyType.Is<Guid>()
            );

            // Method Defaults
            builder.Index.Method.Add<TabNameAttribute>();
            builder.Conventions.SetMethodMetadata(
                attribute: _ => new TabNameAttribute(),
                when: c => c.Method.Has<ActionModelAttribute>(),
                order: int.MaxValue - 5 // TODO fix this hacky order
            );
            builder.Conventions.AddMethodSchema(
                schema: c => MethodRemote(c.Method),
                whenMethod: c => c.Method.Has<ActionModelAttribute>()
            );

            // Parameter Defaults
            builder.Conventions.AddParameterSchema(
                schema: (c, cc) => ParameterParameter(c.Parameter, cc),
                whenParameter: c => c.Parameter.Has<ParameterModelAttribute>()
            );
            builder.Conventions.AddParameterSchemaConfiguration<Parameter>(
                schema: (p, c) =>
                {
                    var api = c.Parameter.Get<ParameterModelAttribute>();

                    p.Required = !api.IsOptional ? true : null;
                    p.DefaultValue = api.DefaultValue;
                },
                whenParameter: c => c.Parameter.Has<ParameterModelAttribute>()
            );

            // Enum Data
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => EnumInline(c.Type, cc),
                whenType: c => c.Type.SkipNullable().IsEnum
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
                whenMethod: c => c.Method.Has<ComponentDescriptorBuilderAttribute<DataTable>>()
            );
            builder.Conventions.AddPropertySchema(
                schema: (c, cc) => PropertyDataTableColumn(c.Property, cc),
                whenProperty: c => c.Property.Has<DataAttribute>()
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
                whenProperty: c => c.Property.Has<DataAttribute>()
            );
        });

        configurator.ConfigureComponentExports(exports =>
        {
            exports.AddFromExtensions(typeof(Components));
        });

        configurator.ConfigureAppDescriptor(app =>
        {
            configurator.UsingLocalization(l =>
            {
                app.Error = ErrorPage(
                    options: ep =>
                    {
                        ep.SafeLinks.AddRange([.. _routes.Where(r => r.ErrorSafeLink).Select(r => r.AsCardLink(l))]);
                        ep.ErrorInfos[403] = ErrorPageInfo(l("Access Denied"), l("You do not have the permision to view the address or data specified."));
                        ep.ErrorInfos[404] = ErrorPageInfo(l("Page Not Found"), l("The page you want to view is etiher deleted or outdated."));
                        ep.ErrorInfos[500] = ErrorPageInfo(l("Unexpected Error"), l("Please contact system administrator."));

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
                layouts.Add(DefaultLayout("default", options: dl =>
                {
                    dl.SideMenu = SideMenu(
                        options: sm =>
                        {
                            sm.Menu.AddRange([.. _routes.Where(r => r.SideMenu).Select(r => r.AsSideMenuItem(l))]);

                            _sideMenuOptions.Apply(sm);
                        }
                    );
                    dl.Header = Header(options: h =>
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

            layouts.Add(ModalLayout("modal"));
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
        });
    }
}