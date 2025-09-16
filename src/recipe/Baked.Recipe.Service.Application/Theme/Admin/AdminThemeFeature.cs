using Baked.Architecture;
using Baked.RestApi.Model;
using Humanizer;

using static Baked.Theme.Admin.Components;
using static Baked.Theme.Admin.DomainComponents;
using static Baked.Theme.Admin.DomainDatas;
using static Baked.Ui.Datas;

namespace Baked.Theme.Admin;

public class AdminThemeFeature(IEnumerable<Route> _routes,
    Action<ErrorPage>? _errorPageOptions = default,
    Action<SideMenu>? _sideMenuOptions = default,
    Action<Header>? _headerOptions = default
) : IFeature<ThemeConfigurator>
{
    public virtual void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => EnumInline(c.Type, cc),
                whenType: c => c.Type.SkipNullable().IsEnum
            );
            builder.Conventions.AddPropertySchema(
                schema: (c, cc) => PropertyConditional(c.Property, cc),
                whenProperty: c => c.Property.IsPublic
            );
            builder.Conventions.AddPropertySchema(
                schema: (c, cc) => PropertyDataTableColumn(c.Property, cc),
                whenProperty: c => c.Property.IsPublic
            );
            builder.Conventions.AddPropertyComponent(
                component: () => String(),
                whenProperty: c => c.Property.IsPublic
            );
            builder.Conventions.AddMethodSchema(
                schema: c => MethodRemote(c.Method),
                whenMethod: c => c.Method.Has<ActionModelAttribute>()
            );
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
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                component: dt =>
                {
                    dt.Schema.Rows = 5;
                    dt.Schema.Paginator = true;
                }
            );

            // NOTE Adds Export support
            builder.Conventions.AddMethodSchema(
                schema: (c, cc) => MethodDataTableExport(c.Method, cc),
                whenMethod: c => c.Method.Has<ComponentDescriptorBuilderAttribute<DataTable>>()
            );
            builder.Conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                schema: (dtc, c, cc) =>
                {
                    var (_, l) = cc;

                    dtc.Title = l(c.Property.Name.Titleize());
                    dtc.Exportable = true;
                },
                whenComponent: c => !c.Path.Contains(nameof(DataTable), nameof(DataTable.Footer))
            );

            // NOTE Numeric property rendering
            builder.Conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                schema: dtc => dtc.AlignRight = true,
                whenProperty: c =>
                    c.Property.IsPublic &&
                    (
                        c.Property.PropertyType.SkipNullable().Is<int>() ||
                        c.Property.PropertyType.SkipNullable().Is<double>() ||
                        c.Property.PropertyType.SkipNullable().Is<decimal>()
                    )
            );
            builder.Conventions.AddPropertyComponent(
                component: () => Number(),
                whenProperty: c => c.Property.IsPublic && c.Property.PropertyType.SkipNullable().Is<int>()
            );
            builder.Conventions.AddPropertyComponent(
                component: () => Money(),
                whenProperty: c => c.Property.IsPublic && c.Property.PropertyType.SkipNullable().Is<decimal>()
            );
            builder.Conventions.AddPropertyComponent(
                component: () => Rate(),
                whenProperty: c => c.Property.IsPublic && c.Property.PropertyType.SkipNullable().Is<double>()
            );

            // NOTE Label property convention
            // TODO Use marks
            builder.Conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                schema: dtc =>
                {
                    dtc.MinWidth = true;
                    dtc.Frozen = true;
                },
                whenProperty: c =>
                    c.Property.PropertyType.Is<string>() &&
                    (
                        // TODO move to hashset and make this parametric
                        c.Property.Name == "Display" ||
                        c.Property.Name == "Label" ||
                        c.Property.Name == "Name" ||
                        c.Property.Name == "Title"
                    )
            );
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                component: dt =>
                {
                    if (dt.Schema.DataKey is not null) { return; }

                    dt.Schema.DataKey = dt.Schema.Columns.FirstOrDefault(c => c.MinWidth == true)?.Prop;
                }
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