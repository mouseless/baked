using Baked.Architecture;
using Baked.Business;
using Baked.RestApi;
using Baked.RestApi.Model;
using Baked.Runtime;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Default.DomainComponents;
using static Baked.Theme.Default.DomainDatas;
using static Baked.Ui.Datas;
using static Baked.Ui.Actions;

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

            // Method Defaults
            builder.Index.Method.Add<TabNameAttribute>();
            builder.Conventions.SetMethodAttribute(
                when: c => c.Method.Has<ActionModelAttribute>(),
                attribute: () => new TabNameAttribute(),
                order: RestApiLayer.MaxConventionOrder + 10
            );
            builder.Conventions.AddMethodSchema(
                when: c => c.Method.Has<ActionModelAttribute>(),
                schema: c => MethodRemote(c.Method)
            );
            builder.Conventions.AddMethodSchemaConfiguration<RemoteData>(
                when: c => c.Type.Has<LocatableAttribute>(),
                schema: rd => rd.Params = Computed.UseRoute("params")
            );
            builder.Conventions.AddMethodSchema(
                when: c => c.Method.Has<ActionModelAttribute>(),
                schema: (c, cc) => MethodContent(c.Method, cc)
            );
            builder.Conventions.AddMethodComponent(
                when: c => c.Method.Has<ActionModelAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(PageTitle.Actions), "*"),
                component: (c, cc) => MethodButton(c.Method, cc)
            );

            // Parameter Defaults
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
                component: c => B.InputText()
            );
            builder.Conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.Is<int>(),
                component: c => B.InputNumber()
            );

            // Enum Data
            builder.Conventions.AddTypeSchema(
                when: c => c.Type.SkipNullable().IsEnum,
                schema: (c, cc) => EnumInline(c.Type, cc)
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
                when: c => c.Method.Has<ComponentDescriptorBuilderAttribute<DataTable>>(),
                schema: (c, cc) => MethodDataTableExport(c.Method, cc)
            );
            builder.Conventions.AddPropertySchema(
                when: c => c.Property.Has<DataAttribute>(),
                schema: (c, cc) => PropertyDataTableColumn(c.Property, cc)
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
            builder.Conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                schema: (dtc, c, cc) =>
                {
                    var data = c.Property.Get<DataAttribute>();

                    var rootProp = cc.Path.Contains(nameof(DataTable.FooterTemplate)) ? "data" : "row";
                    dtc.Component.Data ??= Context.Parent(options: o => o.Prop = $"{rootProp}.{data.Prop}");
                },
                order: UiLayer.MaxConventionOrder - 10
            );

            // Pages
            builder.Conventions.AddTypeComponent(
                where: cc => cc.Path.Is(nameof(Page), "*"),
                component: (c, cc) => TypeTabbedPage(c.Type, cc)
            );
            builder.Conventions.AddTypeComponent(
                where: cc => cc.Path.Is(nameof(Page), "*"),
                component: (c, cc) => TypeSimplePage(c.Type, cc)
            );
            builder.Conventions.AddTypeComponent(
                where: cc => cc.Path.EndsWith(nameof(SimplePage), nameof(SimplePage.Title)),
                component: (c, cc) => TypePageTitle(c.Type, cc)
            );
            builder.Conventions.AddMethodComponent(
                where: cc => cc.Path.Is(nameof(Page), "*", "*"),
                component: (c, cc) => MethodFormPage(c.Method, cc)
            );
            builder.Conventions.AddMethodComponentConfiguration<FormPage>(
                component: (fp, _, cc) =>
                {
                    var (_, l) = cc;

                    fp.Schema.Title.Actions.Add(B.Button(l("Back"),
                        action: Local.UseRedirectBack(),
                        options: b =>
                        {
                            b.Severity = "secondary";
                            b.Variant = "text";
                        })
                    );
                }
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