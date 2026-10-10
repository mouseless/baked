using Baked.Architecture;
using Baked.Business;
using Baked.Core;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Baked.Ui;
using Humanizer;

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
            builder.Index.Type.Add<UiRoute>();
            builder.Index.Property.Add<UiData>();
            builder.Index.Method.Add<UiAction>();
            builder.Index.Method.Add<UiRoute>();

            builder.ConventionOrderMatrix.Bases.Add("Theme");
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            // TYPES

            // configures page route params for types with dynamic page route
            conventions.EditTypeAttribute<UiRoute>(
                when: (c, r) =>
                    r.Path.Contains("[id]") &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.Having<IdProperty>().Any(),
                attribute: (r, c) =>
                {
                    var idAttribute = c.Type.GetMembers().FirstProperty<IdProperty>().Get<IdProperty>();

                    r.Params[idAttribute.RouteName] = idAttribute.RouteName;
                },
                order: Order.At.Infra
            );

            // adds simple page to types
            conventions.AddTypeComponent(
                where: cc => cc.Path.Is("page", "*"),
                component: (_, cc) => B.SimplePage(cc.Route.Path)
            );
            conventions.EditTypeComponent<SimplePage>(
                component: (sp, c, cc) => sp.Schema.Title = c.Type.GenerateRequiredComponent(cc.Drill("simple-page", "title")),
                order: Order.At.Min
            );

            // adds tabbed page to types
            conventions.AddTypeComponent(
                where: cc => cc.Path.Is("page", "*"),
                component: (_, cc) => B.TabbedPage(cc.Route.Path)
            );
            conventions.EditTypeComponent<TabbedPage>(
                component: (sp, c, cc) => sp.Schema.Title = c.Type.GenerateRequiredComponent(cc.Drill("tabbed-page", "title")),
                order: Order.At.Min
            );
            conventions.EditTypeComponent<TabbedPage>(
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
            conventions.EditTypeSchema<Tab>(
                where: cc => cc.Path.EndsWith("tabs", "*"),
                schema: (t, c, cc) =>
                {
                    t.Id = cc.Path.GetParts().Last();
                    t.Icon = c.Type.GenerateComponent(cc.Drill("icon"));
                },
                order: Order.At.Min
            );

            // configures content defaults of type
            conventions.EditTypeSchema<Content>(
                where: cc => cc.Path.EndsWith("contents", "*", "*"),
                schema: (cn, c, cc) =>
                {
                    cn.Key = cc.Path.GetParts().Last();
                    cn.Component = c.Type.GenerateRequiredComponent(cc.Drill(cn.Key, "component"));
                },
                order: Order.At.Min
            );

            // adds enum inline data to enum types
            conventions.AddTypeSchema(
                when: c => c.Type.SkipNullable().IsEnum,
                schema: (c, cc) => EnumInline(c.Type, cc)
            );

            // adds page title to type
            conventions.AddTypeComponent(
                where: cc => cc.Path.Is("page", "*", "*-page", "title"),
                component: () => B.PageTitle()
            );
            conventions.EditTypeComponent<PageTitle>(
                component: (pt, c, cc) =>
                {
                    var (_, l) = cc;

                    pt.Data = Inline(l(cc.Route.Title));
                    pt.Schema.Description = l(cc.Route.Description);
                    pt.Schema.Icon = c.Type.GenerateComponent(cc.Drill("page-title", "icon"));
                },
                order: Order.At.Min
            );
            conventions.EditTypeComponent<PageTitle>(
                component: pt => pt.Schema.LocalizeTitle ??= pt.Data?.RequireLocalization,
                order: Order.At.Global.Max
            );

            // adds action methods to page title actions
            conventions.EditTypeComponent<PageTitle>(
                component: (pt, c, cc) =>
                {
                    foreach (var method in c.Type.GetMembers().Methods.Having<UiAction>())
                    {
                        var action = method.GetAction();
                        if (action.Method == HttpMethod.Get) { continue; }
                        if (method.Has<Initializer>()) { continue; }

                        var actionComponent = method.GenerateComponent(cc.Drill("actions", method.Name));
                        if (actionComponent is null) { continue; }

                        pt.Schema.Actions.Add(actionComponent);
                    }
                },
                order: Order.At.Min
            );

            // configures field set defaults for type
            conventions.EditTypeComponent<Fieldset>(
                when: c => c.Type.HasMembers(),
                component: (f, c, cc) =>
                {
                    cc = cc.Drill("fieldset");

                    var label = c.Type.GetMembers().FirstPropertyOrDefault<Label>();
                    if (label is not null && label.TryGet<UiData>(out var labelData))
                    {
                        f.Schema.TitleProp = labelData.Prop;
                    }

                    f.Data =
                        c.Type.GenerateSchema<InlineData>(cc.Drill("data")) as IData ??
                        c.Type.GenerateSchema<ComputedData>(cc.Drill("data")) as IData ??
                        c.Type.GenerateSchema<RemoteData>(cc.Drill("data"))
                    ;
                },
                order: Order.At.Min
            );

            // configures navlink defaults for routed types
            conventions.EditTypeComponent<NavLink>(
                component: (nl, c, cc) =>
                {
                    if (!c.Type.TryGet<UiRoute>(out var route))
                    {
                        throw DiagnosticCode.TypeWithAttribute.Exception(
                            $"`{nameof(UiRoute)}` is not found on type (`{c.Type.Name}`) to render as `{nameof(NavLink)}`"
                        );
                    }

                    nl.Schema.Path = route.Path;
                },
                order: Order.At.Min
            );

            // PROPERTIES

            // adds data attribute to public properties
            conventions.SetPropertyAttribute(
                when: c => c.Property.IsPublic,
                attribute: c => new UiData(c.Property.Name.Camelize()) { Label = c.Property.Name.Titleize() },
                order: Order.At.Infra - 10
            );

            // hides id data properties
            conventions.EditPropertyAttribute<UiData>(
                when: c => c.Property.Has<IdProperty>(),
                attribute: data => data.Visible = false,
                order: Order.At.Infra
            );

            // adds text component to string, guid, mail address, locatable and value type properties
            conventions.AddPropertyComponent(
                when: c =>
                    c.Property.PropertyType.Is<string>() ||
                    c.Property.PropertyType.SkipNullable().Is<Guid>() ||
                    c.Property.PropertyType.SkipNullable().Is<MailAddress>() ||
                    c.Property.PropertyType.SkipNullable().TryGetMetadata(out var metadata) &&
                    (
                        metadata.Has<Locatable>() ||
                        metadata.Has<Primitive>()
                    ),
                component: () => B.Text(),
                order: Order.At.Min
            );

            // adds text component to enum properties
            conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().IsEnum,
                component: () => B.Text(),
                order: Order.At.Min
            );

            // adds text link to uri properties
            conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().Is<Uri>(),
                component: () => B.TextLink(),
                order: Order.At.Min
            );

            // adds check to boolean properties
            conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().Is<bool>(),
                component: () => B.Check(),
                order: Order.At.Min
            );

            // adds date to date only properties
            conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().Is<DateOnly>(),
                component: () => B.Date(options: td => td.Format = "dd-MM-yyyy"),
                order: Order.At.Min
            );

            // adds date to date time properties
            conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().Is<DateTime>(),
                component: () => B.Date(options: td => td.Format = "dd-MM-yyyy HH:mm:ss"),
                order: Order.At.Min
            );

            // configures data table column for property
            conventions.EditPropertySchema<DataTable.Column>(
                when: c => c.Property.Has<UiData>(),
                schema: (dtc, c, cc) =>
                {
                    dtc.Key = c.Property.DataProp;
                    dtc.Component = c.Property.GenerateRequiredComponent(cc.Drill(dtc.Key, "component"));
                },
                order: Order.At.Min
            );

            // configures field for property
            conventions.EditPropertySchema<Field>(
                schema: (f, c, cc) =>
                {
                    cc = cc.Drill(c.Property.Name);
                    var (_, l) = cc;

                    f.Key = c.Property.Name.Camelize();
                    f.Label = l(c.Property.Name.Titleize());
                    f.Component = c.Property.GenerateRequiredComponent(cc.Drill("component"));
                },
                order: Order.At.Min
            );

            // configures dialog for property
            conventions.EditPropertyComponent<Dialog>(
                component: (d, c, cc) =>
                {
                    cc = cc.Drill("dialog");
                    var (_, l) = cc;

                    d.Schema.Header = l(c.Property.Name.Titleize());
                    d.Schema.Open = c.Property.GenerateRequiredComponent<Button>(cc.Drill("open")).Schema;
                    d.Schema.Content = c.Property.GenerateRequiredComponent(cc.Drill("content"));
                },
                order: Order.At.Min
            );

            // METHODS

            // validates body nullability for GET, DELETE and TRACE remote actions
            conventions.EditMethodSchema<RemoteAction>(
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

            // adds remote data to method
            conventions.AddMethodSchema(
                schema: c => MethodRemote(c.Method)
            );
            conventions.EditMethodSchema<RemoteData>(
                when: c => c.Type.Has<Locatable>(),
                schema: rd => rd.Params = Computed.UseRoute("params")
            );

            // adds remote action to method
            conventions.AddMethodSchema(
                when: c => c.Method.Has<UiAction>(),
                schema: c => DomainActions.MethodRemote(c.Method)
            );

            // configures request body for methods with parameters
            conventions.EditMethodSchema<RemoteAction>(
                when: c => c.Method.DefaultOverload.Parameters.Any(),
                schema: ra => ra.Body = Context.Model()
            );

            // configure route params of actions of locatables on their own pages
            conventions.EditMethodSchema<RemoteAction>(
                when: c => c.Type.Has<Locatable>(),
                where: cc => cc.Path.StartsWith("page", "*", "*-page"),
                schema: (ra, c, cc) =>
                {
                    if (!cc.Path.StartsWith("page", c.Type.Name)) { return; }

                    ra.Params = Computed.UseRoute("params");
                }
            );

            // sets methods as action by default when they are api action
            conventions.SetMethodAttribute(
                when: c => c.Method.Has<ApiAction>(),
                attribute: () => new UiAction(),
                order: Order.At.Theme.AbsoluteMin
            );

            // adds form page to methods
            conventions.AddMethodComponent(
                where: cc => cc.Path.Is("page", "*", "*"),
                component: (_, cc) => B.FormPage(cc.Route.Path)
            );
            conventions.EditMethodComponent<FormPage>(
                component: (fp, c, cc) =>
                {
                    cc = cc.Drill("form-page");

                    fp.Schema.Title = c.Method.GenerateRequiredComponent(cc.Drill("title"));
                    fp.Schema.Submit = c.Method.GenerateRequiredComponent<Button>(cc.Drill("submit")).Schema;
                    fp.Action = c.Method.GenerateRequiredSchema<RemoteAction>(cc.Drill("action"));
                },
                order: Order.At.Min
            );

            // adds page title to method
            conventions.AddMethodComponent(
                where: cc => cc.Path.Is("page", "*", "*", "*-page", "title"),
                component: () => B.PageTitle()
            );
            conventions.EditMethodComponent<PageTitle>(
                component: (pt, c, cc) =>
                {
                    var (_, l) = cc;

                    pt.Data = Inline(l(cc.Route.Title));
                    pt.Schema.Description = l(cc.Route.Description);
                    pt.Schema.Icon = c.Type.GenerateComponent(cc.Drill("page-title", "icon"));
                }
            );
            conventions.EditMethodComponent<PageTitle>(
                component: pt => pt.Schema.LocalizeTitle ??= pt.Data?.RequireLocalization,
                order: Order.At.Global.Max
            );

            // adds content to method
            conventions.AddMethodSchema(
                where: cc => cc.Path.EndsWith("contents", "*"),
                schema: () => B.Content()
            );
            conventions.EditMethodSchema<Content>(
                schema: (cn, c, cc) =>
                {
                    cn.Key = c.Method.Name.Kebaberize();
                    cn.Component = c.Method.GenerateRequiredComponent(cc.Drill(cn.Key, "component"));
                },
                order: Order.At.Min
            );

            // configures data panel defaults for method
            conventions.EditMethodComponent<DataPanel>(
                component: (dp, c, cc) =>
                {
                    dp.Schema.Title = c.Method.GenerateRequiredSchema<InlineData>(cc.Drill("data-panel", "title"));
                    dp.Schema.Content = c.Method.GenerateRequiredComponent(cc.Drill("data-panel", "content"));
                },
                order: Order.At.Min
            );
            conventions.EditMethodComponent<DataPanel>(
                component: dp => dp.Schema.LocalizeTitle ??= dp.Schema.Title.RequireLocalization,
                order: Order.At.Global.Max
            );

            // configures data container defaults for method
            conventions.EditMethodComponent<DataContainer>(
                component: (dp, c, cc) => dp.Schema.Content = c.Method.GenerateRequiredComponent(cc.Drill("data-container", "content")),
                order: Order.At.Min
            );

            // configures data table defaults for method
            conventions.EditMethodComponent<DataTable>(
                component: (dt, c, cc) =>
                {
                    cc = cc.Drill("data-table");

                    dt.Schema.ExportOptions = c.Method.GenerateSchema<DataTable.Export>(cc.Drill("export-options"));
                    dt.Schema.FooterTemplate = c.Method.GenerateSchema<DataTable.Footer>(cc.Drill("footer-template"));
                    dt.Schema.VirtualScrollerOptions = c.Method.GenerateSchema<DataTable.VirtualScroller>(cc.Drill("virtual-scroller-options"));
                    dt.Schema.Actions = c.Method.GenerateSchema<DataTable.Column>(cc.Drill("actions"));

                    dt.Data =
                        c.Method.GenerateSchema<InlineData>(cc.Drill("data")) as IData ??
                        c.Method.GenerateSchema<ComputedData>(cc.Drill("data")) as IData ??
                        c.Method.GenerateSchema<RemoteData>(cc.Drill("data"))
                    ;

                },
                order: Order.At.Min
            );

            // configures actions data table column for method
            conventions.EditMethodSchema<DataTable.Column>(
                where: cc => cc.Path.EndsWith("data-table", "actions"),
                schema: dtc =>
                {
                    dtc.Key = "actions";
                    dtc.Component = B.Composite();
                },
                order: Order.At.Min
            );

            // configures data table export defaults for method
            conventions.EditMethodSchema<DataTable.Export>(
                schema: (dte, c, cc) =>
                {
                    var (_, l) = cc;

                    dte.CsvSeparator = ";";
                    dte.FileName = l($"{c.Method.Name}.ExportFileName");
                },
                order: Order.At.Min
            );

            // configures data table footer defaults for method
            conventions.EditMethodSchema<DataTable.Footer>(
                schema: (dte, c, cc) =>
                {
                    var (_, l) = cc;

                    dte.Label = l($"{c.Method.Name}.FooterLabel");
                },
                order: Order.At.Min
            );

            // configures button defaults for method
            conventions.EditMethodComponent<Button>(
                component: (b, c, cc) =>
                {
                    var (_, l) = cc;

                    b.Schema.Label = l(c.Method.Name.Titleize());
                    b.Action =
                        c.Method.GenerateSchema<LocalAction>(cc.Drill("button", "action")) as IAction ??
                        c.Method.GenerateSchema<PublishAction>(cc.Drill("button", "action")) as IAction ??
                        c.Method.GenerateSchema<RemoteAction>(cc.Drill("button", "action"))
                    ;
                },
                order: Order.At.Min
            );

            // adds simple form to methods
            conventions.AddMethodComponent(
                when: c =>
                    c.Method.TryGet<ApiAction>(out var action) &&
                    action.Method != HttpMethod.Get,
                where: cc => cc.Path.EndsWith("contents", "*", "*", "component"),
                component: () => B.SimpleForm()
            );
            conventions.EditMethodComponent<SimpleForm>(
                component: (sf, c, cc) =>
                {
                    cc = cc.Drill("simple-form");
                    var (_, l) = cc;

                    sf.Action = c.Method.GenerateSchema<RemoteAction>(cc.Drill("action"));
                    sf.Schema.Title = l(c.Method.Name.Titleize());
                    sf.Schema.Submit = c.Method.GenerateRequiredComponent<Button>(cc.Drill("submit")).Schema;
                    sf.Schema.DialogOptions = c.Method.GenerateSchema<SimpleForm.Dialog>(cc.Drill("dialog-options"));

                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        var input = parameter.GenerateSchema<Input>(cc.Drill("inputs"));
                        if (input is null) { continue; }

                        sf.Schema.Inputs.Add(input);
                    }
                },
                order: Order.At.Min
            );

            // configures simple form dialog
            conventions.EditMethodSchema<SimpleForm.Dialog>(
                schema: (sfd, c, cc) =>
                {
                    sfd.Cancel = c.Method.GenerateRequiredComponent<Button>(cc.Drill("cancel")).Schema;
                    sfd.Open = c.Method.GenerateRequiredComponent<Button>(cc.Drill("open")).Schema;
                },
                order: Order.At.Min
            );

            // configures form page defaults for method
            conventions.EditMethodComponent<FormPage>(
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

            // PARAMETERS

            // configures input group key of parameters to their own name by default
            conventions.EditParameterAttribute<Group>(
                attribute: (group, c) => group.InputGroupKey = c.Parameter.Name
            );

            // adds form page input group to parameters
            conventions.AddParameterSchema(
                when: c => c.Parameter.Has<ApiParameter>(),
                schema: () => B.FormPageInputGroup()
            );
            conventions.EditParameterSchema<FormPage.InputGroup>(
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
                when: c => c.Parameter.Has<ApiParameter>(),
                schema: () => B.Input()
            );
            conventions.EditParameterSchema<Input>(
                when: c => c.Parameter.Has<ApiParameter>(),
                schema: (i, c, cc) =>
                {
                    i.Name = c.Parameter.Get<ApiParameter>().Name;
                    i.Component = c.Parameter.GenerateRequiredComponent(cc.Drill(c.Parameter.Name, "component"));
                },
                order: Order.At.Min
            );

            // configures input defaults for parameter
            conventions.EditParameterSchema<Input>(
                schema: (i, c, cc) =>
                {
                    if (i.Component.Schema is not ILabeler labeler) { return; }

                    labeler.Label = c.Parameter.GenerateSchema<Labeler>(cc.Drill(i.Component.Type, "label"));
                },
                order: Order.At.Min
            );
            conventions.EditParameterSchema<Input>(
                schema: i =>
                {
                    if (i.Component.Schema is not ILabeler labeler) { return; }
                    if (labeler.Label?.Text is not null) { return; }

                    labeler.Label = null;
                },
                order: Order.At.Global.Max
            );

            // configures number inputs as numeric
            conventions.EditParameterSchema<Input>(
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
            conventions.EditParameterSchema<Input>(
                when: c => c.Parameter.Has<ApiParameter>(),
                schema: (p, c) =>
                {
                    p.Required = !c.Parameter.IsNullable ? true : null;
                    p.DefaultValue = c.Parameter.DefaultValue;
                }
            );

            conventions.AddParameterSchema(
                schema: () => new Labeler()
            );
            conventions.EditParameterSchema<Labeler>(
                where: cc =>
                    cc.Path.EndsWith("simple-form", "inputs", "*", "label") ||
                    cc.Path.EndsWith("form-page", "**", "inputs", "*", "label"),
                schema: label => label.ShowOptionality = true
            );

            // adds input text to string and value type parameters
            conventions.AddParameterComponent(
                when: c =>
                    c.Parameter.ParameterType.Is<string>() ||
                    c.Parameter.ParameterType.SkipNullable().TryGetMetadata(out var metadata) && metadata.Has<Primitive>(),
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
            conventions.EditParameterComponent<InputCheckbox>(
                when: c =>
                    c.Parameter.ParameterType.Is<bool?>() && c.Parameter.Get<ApiParameter>().FromBodyOrForm,
                component: ic => ic.Schema.Indeterminate = true
            );

            // adds input url to uri parameters
            conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<Uri>(),
                component: () => B.InputUrl(),
                order: Order.At.Min
            );

            // adds input mail address to mail address parameters
            conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<MailAddress>(),
                component: () => B.InputMailAddress(),
                order: Order.At.Min
            );

            // add input date to date only parameters
            conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<DateOnly>(),
                component: () => B.InputDate(options: id =>
                {
                    id.Format = "dd/mm/yy";
                    id.UsePicker = true;
                }),
                order: Order.At.Min
            );

            // `Select` defaults
            conventions.EditParameterComponent<Select>(
                component: (s, c) => s.Schema.ShowClear = c.Parameter.IsNullable ? true : null
            );

            // `SelectButton` defaults
            conventions.EditParameterComponent<SelectButton>(
                component: (s, c) => s.Schema.AllowEmpty = c.Parameter.IsNullable ? true : null
            );
            conventions.EditParameterSchema<Labeler>(
                where: cc => cc.Path.EndsWith("select-button", "label"),
                schema: label =>
                {
                    if (label.Mode == "ifta") { return; }

                    label.None();
                },
                order: Order.At.Global.Max
            );

            // configure select inputs to use inline, computed or remote data
            // from parameter or its parameter type if not configured already
            conventions.EditParameterSchema<Input>(
                schema: (i, c, cc) =>
                {
                    if (i.Component.Schema is not ISelect select) { return; }

                    cc = cc.Drill(i.Component.Schema.GetType().Name, "data");

                    i.Component.Data ??=
                        c.Parameter.GenerateSchema<InlineData>(cc) as IData ??
                        c.Parameter.GenerateSchema<ComputedData>(cc) as IData ??
                        c.Parameter.GenerateSchema<RemoteData>(cc);
                    if (i.Component.Data is null)
                    {
                        if (!c.Parameter.ParameterType.TryGetMetadata(out var metadata))
                        {
                            throw DiagnosticCode.RequiresBuildLevel.Exception(
                                $"{c.Parameter.ParameterType.CSharpFriendlyFullName} cannot be used, its metadata is not present in domain model"
                            );
                        }

                        i.Component.Data =
                            metadata.GenerateSchema<InlineData>(cc) as IData ??
                            metadata.GenerateSchema<ComputedData>(cc) as IData ??
                            metadata.GenerateSchema<RemoteData>(cc) ??
                            throw DiagnosticCode.MissingRequiredSchema.Exception(
                                $"`{c.Parameter.CustomAttributes.Name} or {metadata.CustomAttributes.Name}` is required to have descriptor" +
                                $" for schema type `{nameof(InlineData)}`, `{nameof(ComputedData)}` or `{nameof(RemoteData)}` at path `{cc.Path}`"
                            );
                        ;
                    }

                    select.LocalizeOptionLabels ??= i.Component.Data.RequireLocalization;
                },
                order: Order.At.Global.Max
            );

            // make required select/select-button inputs in data panel to
            // select their first item automatically when they have no default
            conventions.EditParameterSchema<Input>(
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