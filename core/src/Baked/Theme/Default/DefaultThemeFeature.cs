using Baked.Architecture;
using Baked.Business;
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
            // Type defaults
            conventions.AddTypeComponent(
                where: cc => cc.Path.Is(nameof(Page), "*"),
                component: (c, cc) => TypeTabbedPage(c.Type, cc)
            );
            conventions.AddTypeComponent(
                where: cc => cc.Path.Is(nameof(Page), "*"),
                component: (c, cc) => TypeSimplePage(c.Type, cc)
            );
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
                order: Order.At.Defaults
            );

            // Enum Data
            conventions.AddTypeSchema(
                when: c => c.Type.SkipNullable().IsEnum,
                schema: (c, cc) => EnumInline(c.Type, cc)
            );

            // Property defaults
            conventions.SetPropertyAttribute(
                when: c => c.Property.IsPublic,
                attribute: c => new DataAttribute(c.Property.Name.Camelize()) { Label = c.Property.Name.Titleize() },
                order: Order.At.Defaults - 10
            );
            conventions.AddPropertyAttributeConfiguration<DataAttribute>(
                when: c => c.Property.Has<IdAttribute>(),
                attribute: data => data.Visible = false,
                order: Order.At.Defaults
            );
            conventions.AddPropertyComponent(
                when: c =>
                    c.Property.PropertyType.Is<string>() ||
                    c.Property.PropertyType.SkipNullable().Is<Guid>() ||
                    c.Property.PropertyType.SkipNullable().TryGetMetadata(out var metadata) &&
                    (
                        metadata.Has<LocatableAttribute>() ||
                        metadata.Has<ValueTypeAttribute>()
                    ),
                component: () => B.Text(),
                order: Order.At.Theme.Min
            );

            // Method Defaults
            conventions.SetMethodAttribute(
                when: c => c.Method.Has<ActionModelAttribute>(),
                attribute: () => new ActionAttribute(),
                order: Order.At.Theme.AbsoluteMin
            );
            conventions.AddMethodComponent(
                where: cc => cc.Path.Is(nameof(Page), "*", "*"),
                component: (c, cc) => MethodFormPage(c.Method, cc)
            );
            conventions.AddMethodSchema(
                schema: (c, cc) => MethodContent(c.Method, cc)
            );
            conventions.AddMethodSchema(
                schema: c => MethodRemote(c.Method)
            );
            conventions.AddMethodSchemaConfiguration<RemoteData>(
                when: c => c.Type.Has<LocatableAttribute>(),
                schema: rd => rd.Params = Computed.UseRoute("params")
            );
            conventions.AddMethodSchema(
                when: c => c.Method.Has<ActionAttribute>(),
                schema: (c, cc) => DomainActions.MethodRemote(c.Method)
            );
            conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Method.DefaultOverload.Parameters.Any(),
                schema: ra => ra.Body = Context.Model()
            );
            conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Type.Has<LocatableAttribute>(),
                where: cc => cc.Path.StartsWith(nameof(Page), "*", "*Page"),
                schema: (ra, c, cc) =>
                {
                    if (!cc.Path.StartsWith(nameof(Page), c.Type.Name)) { return; }

                    ra.Params = Computed.UseRoute("params");
                }
            );

            conventions.AddMethodComponent(
                when: c =>
                    c.Method.TryGet<ActionModelAttribute>(out var action) &&
                    action.Method != HttpMethod.Get,
                where: cc => cc.Path.EndsWith(nameof(Tab.Contents), "*", nameof(Content.Component)),
                component: (c, cc) => MethodSimpleForm(c.Method, cc)
            );

            conventions.AddMethodComponentConfiguration<SimpleForm>(
                component: (sf, c, cc) =>
                {
                    cc = cc.Drill(nameof(SimpleForm), nameof(SimpleForm.Inputs));

                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        sf.Schema.Inputs.Add(
                            parameter.GenerateRequiredSchema<Input>(cc)
                        );
                    }
                }
            );
            conventions.AddMethodComponentConfiguration<FormPage>(
                component: (fp, c, cc) =>
                {
                    var (_, l) = cc;
                    cc = cc.Drill(nameof(FormPage), nameof(FormPage.Sections));

                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        var section = fp.Schema.Sections.FirstOrDefault(s => s.Key == parameter.SectionKey);
                        if (section is null)
                        {
                            section = B.FormPageSection(parameter.SectionKey, l(parameter.SectionKey.Titleize()));
                            fp.Schema.Sections.Add(section);
                        }

                        section.InputGroups.Add(
                            parameter.GenerateRequiredSchema<FormPage.InputGroup>(
                                cc.Drill(parameter.SectionKey, nameof(FormPage.Section.InputGroups))
                            )
                        );
                    }
                }
            );

            // Parameter defaults
            conventions.AddParameterAttributeConfiguration<GroupAttribute>(
                attribute: (group, c) => group.InputGroupKey = c.Parameter.Name
            );

            conventions.AddParameterSchema(
                when: c => c.Parameter.Has<ParameterModelAttribute>(),
                schema: (c, cc) => ParameterFormPageInputGroup(c.Parameter, cc)
            );

            conventions.AddParameterSchema(
                when: c => c.Parameter.Has<ParameterModelAttribute>(),
                schema: (c, cc) => ParameterInput(c.Parameter, cc)
            );
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
            conventions.AddParameterSchemaConfiguration<Input>(
                when: c => c.Parameter.Has<ParameterModelAttribute>(),
                schema: (p, c) =>
                {
                    p.Required = !c.Parameter.IsNullable ? true : null;
                    p.DefaultValue = c.Parameter.DefaultValue;
                }
            );

            conventions.AddParameterComponent(
                when: c =>
                    c.Parameter.ParameterType.Is<string>() ||
                    c.Parameter.ParameterType.SkipNullable().TryGetMetadata(out var metadata) && metadata.Has<ValueTypeAttribute>(),
                component: (c, cc) => ParameterInputText(c.Parameter, cc),
                order: Order.At.Theme.Min
            );
            conventions.AddParameterComponent(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().Is<int>() ||
                    c.Parameter.ParameterType.SkipNullable().Is<long>(),
                component: (c, cc) => ParameterInputNumber(c.Parameter, cc),
                order: Order.At.Theme.Min
            );

            // `PageTitle` defaults
            conventions.AddTypeComponent(
                where: cc => cc.Path.Is(nameof(Page), "*", "*Page", "Title"),
                component: (c, cc) => TypePageTitle(c.Type, cc)
            );
            conventions.AddTypeComponentConfiguration<PageTitle>(
                component: (pt, c, cc) =>
                {
                    cc = cc.Drill(nameof(PageTitle), nameof(PageTitle.Icon));

                    pt.Schema.Icon = c.Type.GenerateComponent(cc);
                }
            );

            conventions.AddMethodComponent(
                where: cc => cc.Path.Is(nameof(Page), "*", "*", "*Page", "Title"),
                component: (c, cc) => MethodPageTitle(c.Method, cc)
            );
            conventions.AddMethodComponentConfiguration<PageTitle>(
                component: (pt, c, cc) =>
                {
                    cc = cc.Drill(nameof(PageTitle), nameof(PageTitle.Icon));

                    pt.Schema.Icon = c.Method.GenerateComponent(cc);
                }
            );

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
            conventions.AddParameterSchemaConfiguration<Input>(
                schema: i =>
                {
                    if (i.Component.Schema is not SelectButton sb) { return; }
                    if (sb.LabelMode == "ifta") { return; }

                    sb.LabelNone();
                },
                order: Order.At.Max
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