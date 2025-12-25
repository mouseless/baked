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
            builder.Conventions.AddMethodSchemaConfiguration<DataTable.Column>(
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions)),
                schema: (col, c, cc) =>
                {
                    col.Frozen = true;
                    col.AlignRight = true;
                    col.Exportable = false;
                }
            );

            // Simple Form
            builder.Conventions.AddMethodComponent(
                where: cc => cc.Path.EndsWith(nameof(SimpleForm.DialogOptions), nameof(SimpleForm.DialogOptions.Cancel)),
                component: (_, cc) => LocalizedButton("Cancel", cc)
            );
            builder.Conventions.AddMethodComponent(
                where: cc => cc.Path.EndsWith(nameof(SimpleForm.DialogOptions), nameof(SimpleForm.DialogOptions.Open)),
                component: (c, cc) => LocalizedButton(c.Method.Name.Titleize(), cc)
            );

            // `TabbedPage`
            builder.Conventions.AddTypeComponent(
                where: cc => cc.Path.Is(nameof(Page), "*"),
                component: (c, cc) => TypeTabbedPage(c.Type, cc)
            );

            // `SimplePage`
            builder.Conventions.AddTypeComponent(
                where: cc => cc.Path.Is(nameof(Page), "*"),
                component: (c, cc) => TypeSimplePage(c.Type, cc)
            );

            // `FormPage`
            builder.Conventions.AddMethodComponent(
                where: cc => cc.Path.Is(nameof(Page), "*", "*"),
                component: (c, cc) => MethodFormPage(c.Method, cc)
            );

            // `PageTitle`
            builder.Conventions.AddTypeComponent(
                where: cc => cc.Path.Is(nameof(Page), "*", "*Page", "Title"),
                component: (c, cc) => TypePageTitle(c.Type, cc)
            );
            builder.Conventions.AddMethodComponent(
                where: cc => cc.Path.Is(nameof(Page), "*", "*", "*Page", "Title"),
                component: (c, cc) => MethodPageTitle(c.Method, cc)
            );

            // TODO Move to UX

            // actions as buttons ux feature
            builder.Conventions.AddTypeComponentConfiguration<PageTitle>(
                component: (pt, c, cc) =>
                {
                    foreach (var method in c.Type.GetMembers().Methods.Having<ActionAttribute>())
                    {
                        var action = method.GetAction();
                        if (action.Method == HttpMethod.Get) { continue; }

                        var actionComponent = method.GetComponent(cc.Drill(nameof(PageTitle.Actions), method.Name));
                        if (actionComponent is null) { continue; }

                        pt.Schema.Actions.Add(actionComponent);
                    }
                }
            );
            builder.Conventions.AddMethodComponent(
                when: c => c.Method.Has<ActionAttribute>() && !c.Method.DefaultOverload.Parameters.Any(),
                where: cc => cc.Path.EndsWith("Actions", "*"),
                component: (c, cc) => MethodButton(c.Method, cc)
            );
            builder.Conventions.AddMethodComponent(
                when: c => c.Method.Has<ActionAttribute>() && c.Method.DefaultOverload.Parameters.Any(),
                where: cc => cc.Path.EndsWith("Actions", "*"),
                component: (c, cc) => MethodSimpleForm(c.Method, cc)
            );
            builder.Conventions.AddMethodSchema(
                where: cc => cc.Path.EndsWith("Actions", "*", nameof(SimpleForm), nameof(SimpleForm.DialogOptions)),
                schema: (c, cc) => MethodSimpleFormDialog(c.Method, cc)
            );

            builder.Conventions.AddMethodComponent(
                when: c => c.Method.Has<ActionAttribute>(),
                where: cc => cc.Path.EndsWith("Submit"),
                component: (c, cc) => LocalizedButton(c.Method.Name.Titleize(), cc)
            );
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                where: cc => cc.Path.EndsWith("Submit"),
                component: b => b.Schema.Severity = "primary"
            );
            builder.Conventions.AddMethodComponentConfiguration<SimpleForm>(
                component: (sf, c, cc) =>
                {
                    cc = cc.Drill(nameof(SimpleForm.Inputs));

                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        sf.Schema.Inputs.Add(
                            parameter.GetRequiredSchema<Input>(cc.Drill(parameter.Name))
                        );
                    }
                }
            );

            // form page ux feature
            builder.Conventions.AddMethodComponent(
                when: c => c.Method.TryGet<ActionAttribute>(out var action) && action?.RoutePath is not null,
                where: cc => cc.Path.EndsWith("Actions", "*"),
                component: (c, cc) =>
                {
                    var route = c.Method.Get<ActionAttribute>().RoutePath ?? throw new("`RoutePath` can't be null here");

                    return LocalizedButton(c.Method.Name.Titleize(), cc,
                        action: Local.UseRedirect(route)
                    );
                }
            );
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Method.TryGet<ActionAttribute>(out var action) && action.RoutePathBack is not null,
                where: cc => cc.Path.StartsWith(nameof(Page), "*", "*", nameof(FormPage)),
                schema: (ra, c) =>
                {
                    var routeBack = c.Method.Get<ActionAttribute>().RoutePathBack ?? throw new("`RoutePathBack` can't be null here");

                    ra.PostAction = Local.UseRedirect(routeBack);
                }
            );
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                where: cc => cc.Path.EndsWith(nameof(FormPage), nameof(FormPage.Submit)),
                component: (b, _, cc) =>
                {
                    var (_, l) = cc;

                    b.Schema.Label = l("Save");
                }
            );
            builder.Conventions.AddMethodComponentConfiguration<FormPage>(
                component: (fp, c, cc) =>
                {
                    var back = c.Method.GetComponent(cc.Drill(nameof(FormPage), nameof(FormPage.Title), nameof(FormPage.Title.Actions), "Back"));
                    if (back is null) { return; }

                    fp.Schema.Title.Actions.Add(back);
                }
            );
            builder.Conventions.AddMethodComponent(
                where: cc => cc.Path.EndsWith(nameof(FormPage), nameof(FormPage.Title), nameof(FormPage.Title.Actions), "Back"),
                component: (_, cc) => LocalizedButton("Back", cc)
            );
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                where: cc => cc.Path.EndsWith("Back"),
                component: b => b.Action = Local.UseRedirectBack()
            );
            builder.Conventions.AddMethodComponentConfiguration<FormPage>(
                component: (sf, c, cc) =>
                {
                    cc = cc.Drill(nameof(FormPage.Inputs));

                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        sf.Schema.Inputs.Add(
                            parameter.GetRequiredSchema<Input>(cc.Drill(parameter.Name))
                        );
                    }
                }
            );

            // data table related ux
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Method.Has<ActionAttribute>(),
                where: cc => cc.Path.Contains(nameof(DataTable), nameof(DataTable.Actions)),
                schema: ra => ra.Params = Context.Parent(options: o => o.Prop = "row")
            );
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                where: cc =>
                    cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions), "*") ||
                    cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions), "**", nameof(SimpleForm.DialogOptions.Open)),
                component: b =>
                {
                    b.Schema.Label = string.Empty;
                    b.Schema.Variant = "text";
                    b.Schema.Rounded = true;
                }
            );

            // other button related ux
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                when: c => c.Method.Has<ActionAttribute>(),
                where: cc =>
                    !cc.Path.Contains(nameof(FormPage)) &&
                    (
                        cc.Path.EndsWith("Actions", "*") ||
                        cc.Path.EndsWith("*Dialog*", "Open")
                    ),
                component: (b, c) =>
                {
                    var action = c.Method.GetAction();

                    b.Schema.Icon = action.Method switch
                    {
                        var m when m == HttpMethod.Delete => "pi pi-trash",
                        var m when m == HttpMethod.Patch => "pi pi-pencil",
                        var m when m == HttpMethod.Put => "pi pi-pencil",
                        var m when m == HttpMethod.Post && Regexes.StartsWithAddCreateOrNew.IsMatch(c.Method.Name) => "pi pi-plus",
                        _ => b.Schema.Icon
                    };
                    b.Schema.Severity = action.Method == HttpMethod.Delete ? "danger" : b.Schema.Severity;
                }
            );
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                where: cc => cc.Path.EndsWith("Cancel") || cc.Path.EndsWith("Back"),
                component: b => b.Schema.Variant = "text"
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