using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;
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
            // Notes Add tab attribute to all actions
            builder.Conventions.SetMethodMetadata(
                attribute: _ => new TabAttribute(),
                when: c => c.Method.Has<ActionModelAttribute>(),
                order: int.MaxValue - 5
            );

            // NOTE Adds `With` parameters as query parameters of the report page
            builder.Conventions.AddTypeComponentConvention<ReportPage>(
                component: (reportPage, c, cc) =>
                {
                    var members = c.Type.GetMembers();
                    var initializer = members.Methods.Having<InitializerAttribute>().Single();

                    reportPage.Schema.QueryParameters.AddRange(
                        initializer
                            .DefaultOverload.Parameters
                            .Select(p => p.GetRequiredSchema<Parameter>(cc.Drill(nameof(ReportPage.QueryParameters))))
                    );
                },
                whenType: c => c.Type.Has<TransientAttribute>() && c.Type.HasMembers()
            );

            // NOTE Adds `GET` actions under report page tabs as contents
            builder.Conventions.AddTypeSchemaConvention<ReportPage.Tab>(
                schema: (tab, c, cc) =>
                {
                    var members = c.Type.GetMembers();
                    foreach (var method in members.Methods.Having<ActionModelAttribute>())
                    {
                        var action = method.GetAction();
                        if (action.Method != HttpMethod.Get) { continue; }
                        if (!method.TryGet<TabAttribute>(out var group)) { continue; }
                        if (tab.Id != group.Name.Kebaberize()) { continue; }

                        tab.Contents.Add(
                            method.GetRequiredSchema<ReportPage.Tab.Content>(
                                cc.Drill(nameof(ReportPage.Tab.Contents), tab.Contents.Count)
                            )
                        );
                    }
                },
                whenType: c => c.Type.HasMembers()
            );

            // NOTE Adds report page tab content schema to actions
            builder.Conventions.AddMethodSchema(
                schema: (c, cc) => MethodReportPageTabContent(c.Method, cc),
                whenMethod: c => c.Method.Has<ActionModelAttribute>()
            );

            // NOTE Adds add data panel component for actions
            builder.Conventions.AddMethodComponent(
                component: (c, cc) => MethodDataPanel(c.Method, cc),
                whenMethod: c => c.Method.Has<ActionModelAttribute>(),
                whenComponent: c => c.Path.Contains(nameof(ReportPage.Tab.Contents)) && c.Path.EndsWith(nameof(ReportPage.Tab.Content.Component))
            );

            // NOTE Adds method parameters as panel parameters
            builder.Conventions.AddMethodComponentConvention<DataPanel>(
                component: (dp, c, cc) =>
                {
                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        dp.Schema.Parameters.Add(ParameterParameter(parameter, cc.Drill(nameof(DataPanel.Parameters))));
                    }
                }
            );

            // NOTE Adds remote data schema for method
            builder.Conventions.AddMethodSchema(
                schema: c => MethodRemote(c.Method),
                whenMethod: c => c.Method.Has<ActionModelAttribute>()
            );

            // NOTE Adds parameter schema to the `With` parameters of rich transients
            builder.Conventions.AddParameterSchema(
                schema: (c, cc) => ParameterParameter(c.Parameter, cc),
                whenParameter: c => c.Type.Has<TransientAttribute>() && c.Method.Has<InitializerAttribute>()
            );

            // NOTE Parameter with an enum that has <=3 members is represented as `SelectButton`
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => EnumSelectButton(c.Parameter, cc),
                whenParameter: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().GetEnumNames().Count() <= 3
            );

            // NOTE Parameter with an enum that has >3 members is represented as `Select`
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => EnumSelect(c.Parameter, cc),
                whenParameter: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().GetEnumNames().Count() > 3
            );

            // NOTE Default value of a required enum parameter is set to the first enum member
            builder.Conventions.AddParameterSchemaConvention<Parameter>(
                schema: (p, c) => p.DefaultValue = c.Parameter.ParameterType.SkipNullable().GetEnumNames().First(),
                whenParameter: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.TryGet<ParameterModelAttribute>(out var api) &&
                    !api.IsOptional
            );

            // NOTE `Select` and `SelectButton` under tabs is stateful
            builder.Conventions.AddParameterComponentConvention<Select>(
                component: sb => sb.Schema.Stateful = true,
                whenComponent: cc => cc.Path.Contains("Tabs")
            );
            builder.Conventions.AddParameterComponentConvention<SelectButton>(
                component: sb => sb.Schema.Stateful = true,
                whenComponent: cc => cc.Path.Contains("Tabs")
            );

            // NOTE `DataTable` for actions
            builder.Conventions.AddMethodComponent(
                component: (c, cc) => MethodDataTable(c.Method, cc),
                whenMethod: c => c.Method.Has<ActionModelAttribute>() && c.Method.DefaultOverload.ReturnsList(),
                whenComponent: c => c.Path.Matches(Regexes.AnyDataPanelContent)
            );

            // NOTE Adds public properties under data table as columns
            builder.Conventions.AddMethodComponentConvention<DataTable>(
                component: (dt, c, cc) =>
                {
                    var members = c.Method.DefaultOverload.ReturnType.SkipTask().GetElementType().GetMembers();
                    foreach (var property in members.Properties.Where(p => p.IsPublic))
                    {
                        var column = property.GetSchema<DataTable.Column>(cc.Drill(nameof(DataTable.Columns)));
                        if (column is null) { continue; }

                        dt.Schema.Columns.Add(column);
                    }
                },
                whenMethod: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetElementType(out var elementType) &&
                    elementType.HasMembers()
            );

            // NOTE Sets default values for `DataTable`
            builder.Conventions.AddMethodComponentConvention<DataTable>(
                component: dt =>
                {
                    dt.Schema.Rows = 5;
                    dt.Schema.Paginator = true;
                }
            );

            // NOTE `DataTable.Column` for public properties
            builder.Conventions.AddPropertySchema(
                schema: (c, cc) => PropertyDataTableColumn(c.Property, cc),
                whenProperty: c => c.Property.IsPublic
            );
            builder.Conventions.AddPropertySchema(
                schema: (c, cc) => PropertyConditional(c.Property, cc),
                whenProperty: c => c.Property.IsPublic
            );
            builder.Conventions.AddPropertyComponent(
                component: (c, cc) => PropertyString(c.Property, cc),
                whenProperty: c => c.Property.IsPublic
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