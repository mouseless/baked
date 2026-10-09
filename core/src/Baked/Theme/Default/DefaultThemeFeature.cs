using Baked.Architecture;
using Baked.Business;
using Baked.Core;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
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
    ComponentPath.Debug? _debugComponentPaths = default
) : IFeature<ThemeConfigurator>
{
    public virtual void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Type.Add<RouteAttribute>();
            builder.Index.Property.Add<DataAttribute>();
            builder.Index.Method.Add<ActionAttribute>();
            builder.Index.Method.Add<RouteAttribute>();

            builder.ConventionOrderMatrix.Bases.Add("Theme");
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            // configures page route params for types with dynamic page route
            conventions.AddTypeAttributeConfiguration<RouteAttribute>(
                when: (c, r) =>
                    r.Path.Contains("[id]") &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.Having<IdAttribute>().Any(),
                attribute: (r, c) =>
                {
                    var idAttribute = c.Type.GetMembers().FirstProperty<IdAttribute>().Get<IdAttribute>();

                    r.Params[idAttribute.RouteName] = idAttribute.RouteName;
                },
                order: Order.At.Infra
            );

            // adds simple page to types
            conventions.AddTypeComponent(
                where: cc => cc.Path.Is("page", "*"),
                component: (_, cc) => B.SimplePage(cc.Route.Path)
            );
            conventions.AddTypeComponentConfiguration<SimplePage>(
                component: (sp, c, cc) => sp.Schema.Title = c.Type.GenerateRequiredComponent(cc.Drill("simple-page", "title")),
                order: Order.At.Min
            );

            // adds tabbed page to types
            conventions.AddTypeComponent(
                where: cc => cc.Path.Is("page", "*"),
                component: (_, cc) => B.TabbedPage(cc.Route.Path)
            );
            conventions.AddTypeComponentConfiguration<TabbedPage>(
                component: (sp, c, cc) => sp.Schema.Title = c.Type.GenerateRequiredComponent(cc.Drill("tabbed-page", "title")),
                order: Order.At.Min
            );
            conventions.AddTypeComponentConfiguration<TabbedPage>(
               component: (tp, c, cc) =>
               {
                   if (tp.Schema.Tabs.Count <= 1) { return; }

                   var (_, l) = cc;

                   foreach (var tab in tp.Schema.Tabs)
                   {
                       tab.Title ??= l(tab.Id.Replace("-", "_").Titleize());
                   }
               },
               order: Order.At.Global.Max
            );

            // adds tab to type
            conventions.AddTypeSchema(
                where: cc => cc.Path.EndsWith("tabs", "*"),
                schema: () => B.Tab()
            );
            conventions.AddTypeSchemaConfiguration<Tab>(
                where: cc => cc.Path.EndsWith("tabs", "*"),
                schema: (t, c, cc) =>
                {
                    t.Id = cc.Path.GetParts().Last();
                    t.Icon = c.Type.GenerateComponent(cc.Drill("icon"));
                },
                order: Order.At.Min
            );

            // configures content defaults of type
            conventions.AddTypeSchemaConfiguration<Content>(
                where: cc => cc.Path.EndsWith("contents", "*", "*"),
                schema: (cn, c, cc) =>
                {
                    cn.Key = cc.Path.GetParts().Last();
                    cn.Component = c.Type.GenerateRequiredComponent(cc.Drill(cn.Key, "component"));
                },
                order: Order.At.Min
            );

            // Enum Data
            conventions.AddTypeSchema(
                when: c => c.Type.SkipNullable().IsEnum,
                schema: (c, cc) => EnumInline(c.Type, cc)
            );

            // Remote Action
            conventions.AddMethodSchemaConfiguration<RemoteAction>(
                schema: (ra, c) =>
                {
                    var method = ra.Method?.ToUpperInvariant();
                    if (method is null or "GET" or "DELETE" or "TRACE" && ra.Body is not null)
                    {
                        var methodName = method ?? "GET";

                        throw DiagnosticCode.MethodDoesNotSupportBody.Exception(
                            $"{c.Type.Name}.{c.Method.Name}, {methodName} action with a body is not allowed. Remove the body or use a method that supports a payload."
                        );
                    }
                },
                order: Order.At.Global.Max
            );

            // Property defaults
            conventions.SetPropertyAttribute(
                when: c => c.Property.IsPublic,
                attribute: c => new DataAttribute(c.Property.Name.Camelize()) { Label = c.Property.Name.Titleize() },
                order: Order.At.Infra - 10
            );

            conventions.AddPropertyAttributeConfiguration<DataAttribute>(
                when: c => c.Property.Has<IdAttribute>(),
                attribute: data => data.Visible = false,
                order: Order.At.Infra
            );

            conventions.AddPropertyComponent(
                when: c =>
                    c.Property.PropertyType.Is<string>() ||
                    c.Property.PropertyType.SkipNullable().Is<Guid>() ||
                    c.Property.PropertyType.SkipNullable().Is<MailAddress>() ||
                    c.Property.PropertyType.SkipNullable().TryGetMetadata(out var metadata) &&
                    (
                        metadata.Has<LocatableAttribute>() ||
                        metadata.Has<ValueTypeAttribute>()
                    ),
                component: () => B.Text(),
                order: Order.At.Min
            );

            conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().Is<Uri>(),
                component: () => B.TextLink(),
                order: Order.At.Min
            );

            conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().Is<bool>(),
                component: () => B.Check(),
                order: Order.At.Min
            );

            conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().Is<DateOnly>(),
                component: () => B.Date(options: td => td.Format = "dd-MM-yyyy"),
                order: Order.At.Min
            );

            conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().Is<DateTime>(),
                component: () => B.Date(options: td => td.Format = "dd-MM-yyyy HH:mm:ss"),
                order: Order.At.Min
            );

            // configures data table column for property
            conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                when: c => c.Property.Has<DataAttribute>(),
                schema: (dtc, c, cc) =>
                {
                    dtc.Key = c.Property.DataProp;
                    dtc.Component = c.Property.GenerateRequiredComponent(cc.Drill(dtc.Key, "component"));
                },
                order: Order.At.Min
            );

            // Method Defaults

            // adds remote data to method
            conventions.AddMethodSchema(
                schema: c => MethodRemote(c.Method)
            );
            conventions.AddMethodSchemaConfiguration<RemoteData>(
                when: c => c.Type.Has<LocatableAttribute>(),
                schema: rd => rd.Params = Computed.UseRoute("params")
            );

            // adds remote action to method
            conventions.AddMethodSchema(
                when: c => c.Method.Has<ActionAttribute>(),
                schema: c => DomainActions.MethodRemote(c.Method)
            );

            // configures request body for methods with parameters
            conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Method.DefaultOverload.Parameters.Any(),
                schema: ra => ra.Body = Context.Model()
            );

            // configure route params of actions of locatables on their own pages
            conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Type.Has<LocatableAttribute>(),
                where: cc => cc.Path.StartsWith("page", "*", "*-page"),
                schema: (ra, c, cc) =>
                {
                    if (!cc.Path.StartsWith("page", c.Type.Name)) { return; }

                    ra.Params = Computed.UseRoute("params");
                }
            );

            // sets methods as action by default when they are api action
            conventions.SetMethodAttribute(
                when: c => c.Method.Has<ActionModelAttribute>(),
                attribute: () => new ActionAttribute(),
                order: Order.At.Theme.AbsoluteMin
            );

            // adds form page to methods
            conventions.AddMethodComponent(
                where: cc => cc.Path.Is("page", "*", "*"),
                component: (_, cc) => B.FormPage(cc.Route.Path)
            );
            conventions.AddMethodComponentConfiguration<FormPage>(
                component: (fp, c, cc) =>
                {
                    cc = cc.Drill("form-page");

                    fp.Schema.Title = c.Method.GenerateRequiredComponent(cc.Drill("title"));
                    fp.Schema.Submit = c.Method.GenerateRequiredComponent<Button>(cc.Drill("submit")).Schema;
                    fp.Action = c.Method.GenerateRequiredSchema<RemoteAction>(cc.Drill("action"));
                },
                order: Order.At.Min
            );

            // adds content to method
            conventions.AddMethodSchema(
                where: cc => cc.Path.EndsWith("contents", "*"),
                schema: (c, cc) => B.Content()
            );
            conventions.AddMethodSchemaConfiguration<Content>(
                schema: (cn, c, cc) =>
                {
                    cn.Key = c.Method.Name.Kebaberize();
                    cn.Component = c.Method.GenerateRequiredComponent(cc.Drill(cn.Key, "component"));
                },
                order: Order.At.Min
            );

            // configures data panel defaults for method
            conventions.AddMethodComponentConfiguration<DataPanel>(
                component: (dp, c, cc) =>
                {
                    dp.Schema.Title = c.Method.GenerateRequiredSchema<InlineData>(cc.Drill("data-panel", "title"));
                    dp.Schema.Content = c.Method.GenerateRequiredComponent(cc.Drill("data-panel", "content"));
                },
                order: Order.At.Min
            );
            conventions.AddMethodComponentConfiguration<DataPanel>(
                component: dp => dp.Schema.LocalizeTitle ??= dp.Schema.Title.RequireLocalization,
                order: Order.At.Global.Max
            );

            // configures data container defaults for method
            conventions.AddMethodComponentConfiguration<DataContainer>(
                component: (dp, c, cc) => dp.Schema.Content = c.Method.GenerateRequiredComponent(cc.Drill("data-container", "content")),
                order: Order.At.Min
            );

            // configures data table defaults for method
            conventions.AddMethodComponentConfiguration<DataTable>(
                component: (dt, c, cc) =>
                {
                    cc = cc.Drill("data-table");

                    dt.Schema.ExportOptions = c.Method.GenerateSchema<DataTable.Export>(cc.Drill("export-options"));
                    dt.Schema.FooterTemplate = c.Method.GenerateSchema<DataTable.Footer>(cc.Drill("footer-template"));
                    dt.Schema.VirtualScrollerOptions = c.Method.GenerateSchema<DataTable.VirtualScroller>(cc.Drill("virtual-scroller-options"));
                    dt.Schema.Actions = c.Method.GenerateSchema<DataTable.Column>(cc.Drill("actions"));

                    dt.Data =
                        c.Method.GenerateSchema<InlineData>(cc.Drill("data")) as IData ??
                        c.Method.GenerateSchema<RemoteData>(cc.Drill("data"))
                    ;

                },
                order: Order.At.Min
            );

            // configures actions data table column for method
            conventions.AddMethodSchemaConfiguration<DataTable.Column>(
                where: cc => cc.Path.EndsWith("data-table", "actions"),
                schema: dtc =>
                {
                    dtc.Key = "actions";
                    dtc.Component = B.Composite();
                },
                order: Order.At.Min
            );

            // configures data table export defaults for method
            conventions.AddMethodSchemaConfiguration<DataTable.Export>(
                schema: (dte, c, cc) =>
                {
                    var (_, l) = cc;

                    dte.CsvSeparator = ";";
                    dte.FileName = l($"{c.Method.Name}.ExportFileName");
                },
                order: Order.At.Min
            );

            // configures data table footer defaults for method
            conventions.AddMethodSchemaConfiguration<DataTable.Footer>(
                schema: (dte, c, cc) =>
                {
                    var (_, l) = cc;

                    dte.Label = l($"{c.Method.Name}.FooterLabel");
                },
                order: Order.At.Min
            );

            // adds simple form to methods
            conventions.AddMethodComponent(
                when: c =>
                    c.Method.TryGet<ActionModelAttribute>(out var action) &&
                    action.Method != HttpMethod.Get,
                where: cc => cc.Path.EndsWith("contents", "*", "*", "component"),
                component: (c, cc) => MethodSimpleForm(c.Method, cc)
            );
            conventions.AddMethodComponentConfiguration<SimpleForm>(
                component: (sf, c, cc) =>
                {
                    cc = cc.Drill("simple-form", "inputs");

                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        var input = parameter.GenerateSchema<Input>(cc);
                        if (input is null) { continue; }

                        sf.Schema.Inputs.Add(input);
                    }
                }
            );

            // configures form page defaults for method
            conventions.AddMethodComponentConfiguration<FormPage>(
                component: (fp, c, cc) =>
                {
                    var (_, l) = cc;
                    cc = cc.Drill("form-page", "sections");

                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        var section = fp.Schema.Sections.FirstOrDefault(s => s.Key == parameter.SectionKey);
                        if (section is null)
                        {
                            section = B.FormPageSection(parameter.SectionKey, l(parameter.SectionKey.Titleize()));
                            fp.Schema.Sections.Add(section);
                        }

                        var inputGroup = parameter.GenerateSchema<FormPage.InputGroup>(cc.Drill(parameter.SectionKey, "input-groups"));
                        if (inputGroup is null) { continue; }

                        section.InputGroups.Add(inputGroup);
                    }
                },
                order: Order.At.Min
            );

            // Parameter defaults

            // configures input group key of parameters to their own name by default
            conventions.AddParameterAttributeConfiguration<GroupAttribute>(
                attribute: (group, c) => group.InputGroupKey = c.Parameter.Name
            );

            // adds form page input group to parameters
            conventions.AddParameterSchema(
                when: c => c.Parameter.Has<ParameterModelAttribute>(),
                schema: () => B.FormPageInputGroup()
            );
            conventions.AddParameterSchemaConfiguration<FormPage.InputGroup>(
                schema: (fpig, c, cc) =>
                {
                    fpig.Key = c.Parameter.InputGroupKey;
                    fpig.Inputs.Add(
                        c.Parameter.GenerateRequiredSchema<Input>(cc.Drill(fpig.Key, "inputs"))
                    );
                },
                order: Order.At.Min
            );

            // adds input to parameters
            conventions.AddParameterSchema(
                when: c => c.Parameter.Has<ParameterModelAttribute>(),
                schema: (c, cc) => B.Input()
            );
            conventions.AddParameterSchemaConfiguration<Input>(
                when: c => c.Parameter.Has<ParameterModelAttribute>(),
                schema: (i, c, cc) =>
                {
                    i.Name = c.Parameter.Get<ParameterModelAttribute>().Name;
                    i.Component = c.Parameter.GenerateRequiredComponent(cc.Drill(c.Parameter.Name, "component"));
                },
                order: Order.At.Min
            );

            // configures input defaults for parameter
            conventions.AddParameterSchemaConfiguration<Input>(
                schema: (i, c, cc) =>
                {
                    if (i.Component.Schema is not ILabeler labeler) { return; }

                    labeler.Label = c.Parameter.GenerateSchema<Label>(cc.Drill(i.Component.Type, nameof(ILabeler.Label)));
                },
                order: Order.At.Min
            );
            conventions.AddParameterSchemaConfiguration<Input>(
                schema: i =>
                {
                    if (i.Component.Schema is not ILabeler labeler) { return; }
                    if (labeler.Label?.Text is not null) { return; }

                    labeler.Label = null;
                },
                order: Order.At.Global.Max
            );

            // configures number inputs as numeric
            conventions.AddParameterSchemaConfiguration<Input>(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().Is<int>() ||
                    c.Parameter.ParameterType.SkipNullable().Is<decimal>() ||
                    c.Parameter.ParameterType.SkipNullable().Is<double>() ||
                    c.Parameter.ParameterType.SkipNullable().Is<float>() ||
                    c.Parameter.ParameterType.SkipNullable().Is<long>() ||
                    c.Parameter.ParameterType.SkipNullable().Is<short>(),
                schema: input => input.Numeric = true
            );

            // configures default value for required inputs
            conventions.AddParameterSchemaConfiguration<Input>(
                when: c => c.Parameter.Has<ParameterModelAttribute>(),
                schema: (p, c) =>
                {
                    p.Required = !c.Parameter.IsNullable ? true : null;
                    p.DefaultValue = c.Parameter.DefaultValue;
                }
            );

            conventions.AddParameterSchema(
                schema: () => new Label()
            );
            conventions.AddParameterSchemaConfiguration<Label>(
                where: cc =>
                    cc.Path.EndsWith(nameof(SimpleForm), nameof(SimpleForm.Inputs), "*", nameof(ILabeler.Label)) ||
                    cc.Path.EndsWith(nameof(FormPage), "**", nameof(FormPage.InputGroup.Inputs), "*", nameof(ILabeler.Label)),
                schema: label => label.ShowOptionality = true
            );

            // adds input text to string and value type parameters
            conventions.AddParameterComponent(
                when: c =>
                    c.Parameter.ParameterType.Is<string>() ||
                    c.Parameter.ParameterType.SkipNullable().TryGetMetadata(out var metadata) && metadata.Has<ValueTypeAttribute>(),
                component: () => B.InputText(),
                order: Order.At.Min
            );

            // adds input number to int and long parameters
            conventions.AddParameterComponent(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().Is<int>() ||
                    c.Parameter.ParameterType.SkipNullable().Is<long>(),
                component: () => B.InputNumber(),
                order: Order.At.Min
            );

            // adds input money to decimal parameters
            conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<decimal>(),
                component: () => B.InputMoney(),
                order: Order.At.Min
            );

            // adds input rate to double parameters
            conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<double>(),
                component: () => B.InputRate(),
                order: Order.At.Min
            );

            // adds input checkbox to bool parameters
            conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<bool>(),
                component: () => B.InputCheckbox(),
                order: Order.At.Min
            );
            conventions.AddParameterComponentConfiguration<InputCheckbox>(
                when: c =>
                    c.Parameter.ParameterType.Is<bool?>() && c.Parameter.Get<ParameterModelAttribute>().FromBodyOrForm,
                component: ic => ic.Schema.Indeterminate = true
            );

            // adds input url to uri parameters
            conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<Uri>(),
                component: (c, cc) => B.InputUrl(),
                order: Order.At.Min
            );

            // adds input mail address to mail address parameters
            conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<MailAddress>(),
                component: (c, cc) => B.InputMailAddress(),
                order: Order.At.Min
            );

            // add input date to date only parameters
            conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<DateOnly>(),
                component: (c, cc) => B.InputDate(options: id =>
                {
                    id.Format = "dd/mm/yy";
                    id.UsePicker = true;
                }),
                order: Order.At.Min
            );

            // adds page title to type
            conventions.AddTypeComponent(
                where: cc => cc.Path.Is("page", "*", "*-page", "title"),
                component: () => B.PageTitle()
            );
            conventions.AddTypeComponentConfiguration<PageTitle>(
                component: (pt, c, cc) =>
                {
                    var (_, l) = cc;

                    pt.Data = Inline(l(cc.Route.Title));
                    pt.Schema.Description = l(cc.Route.Description);
                    pt.Schema.Icon = c.Type.GenerateComponent(cc.Drill("page-title", "icon"));
                }
            );
            conventions.AddTypeComponentConfiguration<PageTitle>(
                component: pt => pt.Schema.LocalizeTitle ??= pt.Data?.RequireLocalization,
                order: Order.At.Global.Max
            );

            // adds page title to method
            conventions.AddMethodComponent(
                where: cc => cc.Path.Is("page", "*", "*", "*-page", "title"),
                component: () => B.PageTitle()
            );
            conventions.AddMethodComponentConfiguration<PageTitle>(
                component: (pt, c, cc) =>
                {
                    var (_, l) = cc;

                    pt.Data = Inline(l(cc.Route.Title));
                    pt.Schema.Description = l(cc.Route.Description);
                    pt.Schema.Icon = c.Type.GenerateComponent(cc.Drill("page-title", "icon"));
                }
            );
            conventions.AddMethodComponentConfiguration<PageTitle>(
                component: pt => pt.Schema.LocalizeTitle ??= pt.Data?.RequireLocalization,
                order: Order.At.Global.Max
            );

            // adds action methods to page title actions
            conventions.AddTypeComponentConfiguration<PageTitle>(
                component: (pt, c, cc) =>
                {
                    foreach (var method in c.Type.GetMembers().Methods.Having<ActionAttribute>())
                    {
                        var action = method.GetAction();
                        if (action.Method == HttpMethod.Get) { continue; }
                        if (method.Has<InitializerAttribute>()) { continue; }

                        var actionComponent = method.GenerateComponent(cc.Drill(nameof(PageTitle.Actions), method.Name));
                        if (actionComponent is null) { continue; }

                        pt.Schema.Actions.Add(actionComponent);
                    }
                }
            );

            // `Select` defaults
            conventions.AddParameterComponentConfiguration<Select>(
                component: (s, c) => s.Schema.ShowClear = c.Parameter.IsNullable ? true : null
            );

            // `SelectButton` defaults
            conventions.AddParameterComponentConfiguration<SelectButton>(
                component: (s, c) => s.Schema.AllowEmpty = c.Parameter.IsNullable ? true : null
            );
            conventions.AddParameterSchemaConfiguration<Label>(
                where: cc => cc.Path.EndsWith(nameof(SelectButton), nameof(ILabeler.Label)),
                schema: label =>
                {
                    if (label.Mode == "ifta") { return; }

                    label.None();
                },
                order: Order.At.Global.Max
            );

            // configure select inputs to use inline data or remote from parameter or parameter type if not already configured
            conventions.AddParameterSchemaConfiguration<Input>(
                schema: (i, c, cc) =>
                {
                    if (i.Component.Schema is not ISelect select) { return; }

                    cc = cc.Drill(i.Component.Schema.GetType().Name, "data");

                    i.Component.Data ??=
                        c.Parameter.GenerateSchema<InlineData>(cc) as IData ??
                        c.Parameter.GenerateSchema<RemoteData>(cc) ??
                        null;
                    if (!c.Parameter.ParameterType.TryGetMetadata(out var metadata))
                    {
                        throw DiagnosticCode.RequiresBuildLevel.Exception(
                            $"{c.Parameter.ParameterType.CSharpFriendlyFullName} cannot be used, its metadata is not present in domain model"
                        );
                    }

                    i.Component.Data ??=
                        metadata.GenerateSchema<InlineData>(cc) as IData ??
                        metadata.GenerateSchema<RemoteData>(cc) ??
                        throw DiagnosticCode.MissingRequiredSchema.Exception(
                            $"`{c.Parameter.CustomAttributes.Name} or {metadata.CustomAttributes.Name}` is required to have descriptor" +
                            $" for schema type `{nameof(InlineData)}` or `{nameof(RemoteData)}` at path `{cc.Path}`"
                        );
                    ;
                    select.LocalizeOptionLabels ??= i.Component.Data.RequireLocalization;
                },
                order: Order.At.Global.Max
            );

            // make required select/select-button inputs in data panel to
            // select their first item automatically when they have no default
            conventions.AddParameterSchemaConfiguration<Input>(
                when: c => !c.Parameter.IsNullable,
                where: c => c.Path.EndsWith("data-panel", "inputs"),
                schema: i =>
                {
                    if (i.Default is not null) { return; }
                    if (i.Component.Schema is SelectButton sb)
                    {
                        sb.AutoSelectFirst = true;
                        i.DefaultSelfManaged = true;
                    }

                    if (i.Component.Schema is Select s)
                    {
                        s.AutoSelectFirst = true;
                        i.DefaultSelfManaged = true;
                    }
                },
                order: Order.At.Global.Max
            );
        });

        configurator.Ui.ConfigureComponentExports(exports =>
        {
            exports.AddFromExtensions(typeof(B));
        });

        configurator.Ui.ConfigureAppDescriptor(app =>
        {
            configurator.Ui.UsingLocalization(l =>
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
                        ep.ErrorInfos[999] = B.ErrorPageInfo(l("Application Error"), l("Please contact system administrator."));

                        _errorPageOptions.Apply(ep);
                    },
                    data: Computed.UseError()
                );
                app.InlineError = B.Message(
                    data: default(IData),
                    options: m =>
                    {
                        m.Icon = "pi pi-exclamation-circle";
                        m.Severity = "error";
                    }
                );
            });
        });

        configurator.Ui.ConfigureLayoutDescriptors(layouts =>
        {
            configurator.Ui.UsingLocalization(l =>
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

        configurator.Ui.ConfigurePageDescriptors(pages =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                configurator.Ui.UsingLocalization(l =>
                {
                    pages.AddPages(_routes, domain, l,
                        debugComponentPaths: _debugComponentPaths
                    );
                });
            });
        });
    }
}