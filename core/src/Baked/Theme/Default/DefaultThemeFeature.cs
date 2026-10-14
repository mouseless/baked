using Baked.Architecture;
using Baked.Business;
using Baked.Core;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.RestApi.Model;
using Baked.Runtime;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;
using Newtonsoft.Json;

namespace Baked;

public static class DefaultThemeFeature
{
    public static JsonSerializerSettings JsonSettings => new()
    {
        ContractResolver = new AttributeAwareCamelCasePropertyNamesContractResolver(),
        NullValueHandling = NullValueHandling.Ignore,
    };

    public static IFeature<ThemeFeature.Configurator> Default(this ThemeFeature.Configurator x,
        Func<Router, Route> index,
        IEnumerable<Func<Router, Route>>? routes = default,
        Action<ErrorPage>? errorPageOptions = default,
        Action<SideMenu>? sideMenuOptions = default,
        Action<Header>? headerOptions = default,
        ComponentPath.Debug? debugComponentPaths = default,
        Action<LayerConfigurator>? configure = default
    ) => x.Define(BuildRoutes(index, routes), (configurator, routes) =>
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
            conventions.AddTypeComponent<SimplePage>(
                where: cc => cc.Path.Is("page", "*")
            );
            conventions.EditTypeComponent<SimplePage>(
                component: (sp, c, cc) => sp.Schema.Title = c.Type.GenerateRequiredComponent(cc.Drill("simple-page", "title")),
                order: Order.At.Min
            );

            // adds tabbed page to types
            conventions.AddTypeComponent<TabbedPage>(
                where: cc => cc.Path.Is("page", "*")
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
            conventions.AddTypeSchema<Tab>(
                where: cc => cc.Path.EndsWith("tabs", "*")
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
            conventions.AddTypeSchema<InlineData>(
                when: c => c.Type.SkipNullable().IsEnum
            );
            conventions.EditTypeSchema<InlineData>(
                when: c => c.Type.SkipNullable().IsEnum,
                schema: (id, c, cc) =>
                {
                    var (_, l) = cc;

                    id.Value = c.Type.SkipNullable().GetEnumNames().Select(name => new
                    {
                        label = l($"{c.Type.SkipNullable().Name}.{name}"),
                        value = name.Camelize()
                    });
                    id.RequireLocalization = true;
                },
                order: Order.At.Min
            );

            // adds page title to type
            conventions.AddTypeComponent<PageTitle>(
                where: cc => cc.Path.Is("page", "*", "*-page", "title")
            );
            conventions.EditTypeComponent<PageTitle>(
                component: (pt, c, cc) =>
                {
                    var (_, l) = cc;

                    pt.Data = Fetch.Inline(l(cc.Route.Title));
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

            // configures route icon to type avatar
            conventions.AddTypeComponent<Avatar>(
                when: c => c.Type.Has<Locatable>() || c.Type.Has<Singleton>(),
                where: cc =>
                    cc.Route.Icon is not null &&
                    cc.Path.Is("page", "*", "simple-page", "**", "page-title", "icon")
            );
            conventions.EditTypeComponent<Avatar>(
                where: cc => cc.Path.EndsWith("page-title", "icon"),
                component: (a, _, cc) =>
                {
                    a.Schema.Icon ??= cc.Route.Icon;
                    a.Schema.Size ??= "xlarge";
                }
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
            conventions.AddPropertyComponent<Text>(
                when: c =>
                    c.Property.PropertyType.Is<string>() ||
                    c.Property.PropertyType.SkipNullable().Is<Guid>() ||
                    c.Property.PropertyType.SkipNullable().Is<MailAddress>() ||
                    c.Property.PropertyType.SkipNullable().TryGetMetadata(out var metadata) &&
                    (
                        metadata.Has<Locatable>() ||
                        metadata.Has<Primitive>()
                    ),
                order: Order.At.Min
            );

            // adds tag component to enum properties
            conventions.AddPropertyComponent<Tag>(
                when: c => c.Property.PropertyType.SkipNullable().IsEnum,
                order: Order.At.Min
            );

            // configures enum localization for tag
            conventions.EditPropertyComponent<Tag>(
                when: c => c.Property.PropertyType.SkipNullable().IsEnum,
                component: (t, c, cc) =>
                {
                    // NOTE forces enum localizations to get hit
                    var schema = c.Property.PropertyType.SkipNullable().GetMetadata().GenerateSchema<InlineData>(cc.Drill("data"));
                    schema?.ToJsonString();

                    t.Schema.LocalizationPrefix = c.Property.PropertyType.SkipNullable().Name;
                    t.Schema.LocalizeText = true;
                },
                order: Order.At.Min
            );

            // adds text link to uri properties
            conventions.AddPropertyComponent<TextLink>(
                when: c => c.Property.PropertyType.SkipNullable().Is<Uri>(),
                order: Order.At.Min
            );

            // adds check to boolean properties
            conventions.AddPropertyComponent<Check>(
                when: c => c.Property.PropertyType.SkipNullable().Is<bool>(),
                order: Order.At.Min
            );

            // adds date to date only properties
            conventions.AddPropertyComponent<Date>(
                when: c => c.Property.PropertyType.SkipNullable().Is<DateOnly>(),
                order: Order.At.Min
            );
            conventions.EditPropertyComponent<Date>(
                when: c => c.Property.PropertyType.SkipNullable().Is<DateOnly>(),
                component: cd => cd.Schema.Format = "dd-MM-yyyy",
                order: Order.At.Min
            );

            // adds date to date time properties
            conventions.AddPropertyComponent<Date>(
                when: c => c.Property.PropertyType.SkipNullable().Is<DateTime>(),
                order: Order.At.Min
            );
            conventions.EditPropertyComponent<Date>(
                when: c => c.Property.PropertyType.SkipNullable().Is<DateTime>(),
                component: cd => cd.Schema.Format = "dd-MM-yyyy HH:mm:ss",
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
            conventions.AddMethodSchema<RemoteData>();
            conventions.EditMethodSchema<RemoteData>(
                schema: (rd, c) =>
                {
                    rd.Path = c.Method.GetAction().GetRoute();
                    rd.Query = Fetch.Computed.UseRoute("query");
                    if (c.Method.DefaultOverload.Parameters.Any())
                    {
                        rd.Query += Fetch.Context.Parent(options: cd => cd.Prop = "parameters");
                    }
                },
                order: Order.At.Min
            );
            conventions.EditMethodSchema<RemoteData>(
                when: c => c.Type.Has<Locatable>(),
                schema: rd => rd.Params = Fetch.Computed.UseRoute("params")
            );

            // adds remote action to method
            conventions.AddMethodSchema<RemoteAction>(
                when: c => c.Method.Has<UiAction>()
            );
            conventions.EditMethodSchema<RemoteAction>(
                schema: (ra, c) =>
                {
                    ra.Path = c.Method.GetAction().GetRoute();
                    ra.Method = c.Method.GetAction().Method.Method;
                    ra.PostAction = Execute.Publish.Event(c.Method.Name.Kebaberize(),
                        options: ea => ea.Data = c.Method.DefaultOverload.ReturnsVoid()
                            ? null
                            : Fetch.Context.Response()
                    );
                },
                order: Order.At.Min
            );

            // configures defaults for any inline data added for a method
            conventions.EditMethodSchema<InlineData>(
                schema: (id, c, cc) =>
                {
                    var (_, l) = cc;

                    id.Value = l(c.Method.Name);
                },
                order: Order.At.Min
            );

            // configures request body for methods with parameters
            conventions.EditMethodSchema<RemoteAction>(
                when: c => c.Method.DefaultOverload.Parameters.Any(),
                schema: ra => ra.Body = Fetch.Context.Model()
            );

            // configure route params of actions of locatables on their own pages
            conventions.EditMethodSchema<RemoteAction>(
                when: c => c.Type.Has<Locatable>(),
                where: cc => cc.Path.StartsWith("page", "*", "*-page"),
                schema: (ra, c, cc) =>
                {
                    if (!cc.Path.StartsWith("page", c.Type.Name)) { return; }

                    ra.Params = Fetch.Computed.UseRoute("params");
                }
            );

            // sets methods as action by default when they are api action
            conventions.SetMethodAttribute(
                when: c => c.Method.Has<ApiAction>(),
                attribute: () => new UiAction(),
                order: Order.At.Theme.AbsoluteMin
            );

            // adds form page to methods
            conventions.AddMethodComponent<FormPage>(
                where: cc => cc.Path.Is("page", "*", "*")
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
            conventions.AddMethodComponent<PageTitle>(
                where: cc => cc.Path.Is("page", "*", "*", "*-page", "title")
            );
            conventions.EditMethodComponent<PageTitle>(
                component: (pt, c, cc) =>
                {
                    var (_, l) = cc;

                    pt.Data = Fetch.Inline(l(cc.Route.Title));
                    pt.Schema.Description = l(cc.Route.Description);
                    pt.Schema.Icon = c.Type.GenerateComponent(cc.Drill("page-title", "icon"));
                }
            );
            conventions.EditMethodComponent<PageTitle>(
                component: pt => pt.Schema.LocalizeTitle ??= pt.Data?.RequireLocalization,
                order: Order.At.Global.Max
            );

            // configures route icon to method avatar
            conventions.EditMethodComponent<Avatar>(
                where: cc => cc.Path.EndsWith("page-title", "icon"),
                component: (a, _, cc) =>
                {
                    a.Schema.Icon ??= cc.Route.Icon;
                    a.Schema.Size ??= "xlarge";
                }
            );

            // adds content to method
            conventions.AddMethodSchema<Content>(
                where: cc => cc.Path.EndsWith("contents", "*")
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
                    dtc.Component = new Composite().ToDescriptor();
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
                    cc = cc.Drill("button");
                    var (_, l) = cc;

                    b.Schema.Label = l(c.Method.Name.Titleize());
                    b.Schema.DialogOptions = c.Method.GenerateSchema<Button.Dialog>(cc.Drill("dialog-options"));
                    b.Action =
                        b.Schema.DialogOptions?.Form is not null ? Execute.Publish.Event(c.Method.Name.Kebaberize(), options: pa => pa.Data = null) :
                        c.Method.GenerateSchema<LocalAction>(cc.Drill("action")) as IAction ??
                        c.Method.GenerateSchema<PublishAction>(cc.Drill("action")) as IAction ??
                        c.Method.GenerateSchema<RemoteAction>(cc.Drill("action"))
                    ;
                },
                order: Order.At.Min
            );

            // configures button dialog defaults for method
            conventions.EditMethodSchema<Button.Dialog>(
                schema: (bd, c, cc) =>
                {
                    var (_, l) = cc;

                    bd.Header = l(c.Method.Name.Titleize());
                    bd.Form = c.Method.GenerateComponent(cc.Drill("form"));

                    if (bd.Form is null)
                    {
                        bd.Content = c.Method.GenerateComponent(cc.Drill("content"));
                    }

                    if (bd.Form is null && bd.Content is null)
                    {
                        throw DiagnosticCode.MissingRequiredComponent.Exception(
                            $"Button.Dialog requires either Form or Content to be configured at path `{cc.Path}` for method `{c.Method.CustomAttributes.Name}`"
                        );
                    }

                    if (bd.Content is not null)
                    {
                        bd.Submit = c.Method.GenerateRequiredComponent<Button>(cc.Drill("submit")).Schema;
                    }

                    bd.Cancel = c.Method.GenerateComponent<Button>(cc.Drill("cancel"))?.Schema;
                },
                order: Order.At.Min
            );

            // adds simple form to methods under content component
            conventions.AddMethodComponent<SimpleForm>(
                when: c =>
                    c.Method.TryGet<ApiAction>(out var action) &&
                    action.Method != HttpMethod.Get,
                where: cc => cc.Path.EndsWith("contents", "*", "*", "component")
            );
            conventions.EditMethodComponent<SimpleForm>(
                component: (sf, c, cc) =>
                {
                    cc = cc.Drill("simple-form");

                    sf.Action = c.Method.GenerateSchema<RemoteAction>(cc.Drill("action"));
                    sf.Schema.Submit = c.Method.GenerateRequiredComponent<Button>(cc.Drill("submit")).Schema;

                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        var input = parameter.GenerateSchema<Input>(cc.Drill("inputs"));
                        if (input is null) { continue; }

                        sf.Schema.Inputs.Add(input);
                    }
                },
                order: Order.At.Min
            );

            // configures title for simple form directly under content component
            conventions.EditMethodComponent<SimpleForm>(
                where: cc => cc.Path.EndsWith("contents", "*", "*", "component"),
                component: (sf, c, cc) =>
                {
                    var (_, l) = cc;

                    sf.Schema.Title = l(c.Method.Name.Titleize());
                }
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
                            section = new FormPage.Section
                            {
                                Key = parameter.SectionKey,
                                Label = l(parameter.SectionKey.Titleize())
                            };

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
            conventions.AddParameterSchema<FormPage.InputGroup>(
                when: c => c.Parameter.Has<ApiParameter>()
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
            conventions.AddParameterSchema<Input>(
                when: c => c.Parameter.Has<ApiParameter>()
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
                    p.DefaultValue =
                        c.Parameter.ParameterType.IsEnum ? c.Parameter.DefaultValue?.ToString()?.Camelize() :
                        c.Parameter.DefaultValue;
                }
            );

            // adds labeler to parameter schema
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
            conventions.AddParameterComponent<InputText>(
                when: c =>
                    c.Parameter.ParameterType.Is<string>() ||
                    c.Parameter.ParameterType.SkipNullable().TryGetMetadata(out var metadata) && metadata.Has<Primitive>(),
                order: Order.At.Min
            );

            // adds input number to int and long parameters
            conventions.AddParameterComponent<InputNumber>(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().Is<int>() ||
                    c.Parameter.ParameterType.SkipNullable().Is<long>(),
                order: Order.At.Min
            );

            // adds input money to decimal parameters
            conventions.AddParameterComponent<InputMoney>(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<decimal>(),
                order: Order.At.Min
            );

            // adds input rate to double parameters
            conventions.AddParameterComponent<InputRate>(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<double>(),
                order: Order.At.Min
            );

            // adds input checkbox to bool parameters
            conventions.AddParameterComponent<InputCheckbox>(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<bool>(),
                order: Order.At.Min
            );
            conventions.EditParameterComponent<InputCheckbox>(
                when: c =>
                    c.Parameter.ParameterType.Is<bool?>() && c.Parameter.Get<ApiParameter>().FromBodyOrForm,
                component: ic => ic.Schema.Indeterminate = true
            );

            // adds input url to uri parameters
            conventions.AddParameterComponent<InputUrl>(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<Uri>(),
                order: Order.At.Min
            );

            // adds input mail address to mail address parameters
            conventions.AddParameterComponent<InputMailAddress>(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<MailAddress>(),
                order: Order.At.Min
            );

            // add input date to date only parameters
            conventions.AddParameterComponent<InputDate>(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<DateOnly>(),
                order: Order.At.Min
            );
            conventions.EditParameterComponent<InputDate>(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<DateOnly>(),
                component: cd =>
                {
                    cd.Schema.Format = "dd/mm/yy";
                    cd.Schema.UsePicker = true;
                },
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

        configurator.Buildtime.ConfigureGeneratedFileCollection(files =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                var severities = new Dictionary<string, Dictionary<string, string>>();
                foreach (var type in domain.Types.Having<Severities>())
                {
                    severities[type.Name] = type.GetMetadata().Get<Severities>().Value;
                }

                files.AddAsJson("severities", severities,
                    outdir: "Ui",
                    settings: JsonSettings
                );
            });
        });

        configurator.Ui.ConfigureComponentExports(exports =>
        {
            exports.AddFromThemeAssembly(typeof(DefaultThemeFeature));
        });

        configurator.Ui.ConfigureAppDescriptor(app =>
        {
            configurator.Ui.UsingLocalization(l =>
            {
                var error = new ErrorPage
                {
                    SafeLinks = [.. routes.Where(r => r.ErrorSafeLink).Select(r => r.ToCardLink(l))],
                    ErrorInfos =
                    {
                        [403] = new()
                        {
                            Title = l("Access Denied"),
                            Message = l("You do not have the permission to view the address or data specified."),
                            ShowSafeLinks = true
                        },
                        [404] = new()
                        {
                            Title = l("Page Not Found"),
                            Message = l("The page you want to view is either deleted or outdated."),
                            ShowSafeLinks = true
                        },
                        [500] = new() { Title = l("Unexpected Error"), Message = l("Please contact system administrator.") },
                        [999] = new() { Title = l("Application Error"), Message = l("Please contact system administrator.") }
                    }
                };
                errorPageOptions.Apply(error);

                app.Error = error.ToDescriptor(data: Fetch.Computed.UseError());
                app.InlineError = new Message
                {
                    Icon = "pi pi-exclamation-circle",
                    Severity = "error"
                }.ToDescriptor();
            });
        });

        configurator.Ui.ConfigureLayoutDescriptors(layouts =>
        {
            configurator.Ui.UsingLocalization(l =>
            {
                var sideMenu = new SideMenu
                {
                    Menu = [.. routes.Where(r => r.SideMenu).Select(r => r.ToSideMenuItem(l))]
                };
                sideMenuOptions.Apply(sideMenu);

                var header = new Header();
                foreach (var route in routes)
                {
                    if (route.Disabled) { continue; }

                    header.Sitemap[route.Path] = route.ToHeaderItem(l);
                }

                headerOptions.Apply(header);

                layouts.AddLayout(new DefaultLayout
                {
                    Path = "default",
                    SideMenu = sideMenu.ToDescriptor(data: Fetch.Computed.UseRoute()),
                    Header = header.ToDescriptor(data: Fetch.Computed.UseRoute())
                });
            });

            layouts.AddLayout(new ModalLayout { Path = "modal" });
        });

        configurator.Ui.ConfigurePageDescriptors(pages =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                configurator.Ui.UsingLocalization(l =>
                {
                    pages.AddPages(routes, domain, l,
                        debugComponentPaths: debugComponentPaths
                    );
                });
            });
        });

        configure.Apply(configurator);
    });

    static List<Route> BuildRoutes(Func<Router, Route> index, IEnumerable<Func<Router, Route>>? routes) =>
        [index(new()), .. routes?.Select(r => r(new())) ?? []];

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
            new CardLink
            {
                Route = route.Path,
                Title = l(route.Title),
                Icon = route.Icon,
                Description = l(route.Description),
                Disabled = route.Disabled ? true : null,
                DisabledReason = l(route.DisabledReason)
            }.ToDescriptor();

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