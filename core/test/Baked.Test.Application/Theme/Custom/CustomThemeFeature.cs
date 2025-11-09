using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Test.Caching;
using Baked.Test.Ui;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;

using static Baked.Test.Theme.Custom.DomainComponents;
using static Baked.Theme.Default.DomainComponents;
using static Baked.Theme.Default.DomainDatas;

using B = Baked.Ui.Components;
using C = Baked.Test.Ui.Components;
using Route = Baked.Theme.Route;

namespace Baked.Test.Theme.Custom;

public class CustomThemeFeature(IEnumerable<Func<Router, Route>> routes)
    : DefaultThemeFeature(routes.Select(r => r(new())),
        _sideMenuOptions: sm => sm.Footer = B.LanguageSwitcher()
    )
{
    public override void Configure(LayerConfigurator configurator)
    {
        base.Configure(configurator);

        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // Custom theme CSV formatter settings
            builder.Conventions.AddMethodSchemaConfiguration<DataTable.Export>(
                schema: (dte, _, cc) =>
                {
                    var (_, l) = cc;

                    dte.ButtonLabel = l("Export as CSV");
                    dte.Formatter = "useCsvFormatter";
                    dte.AppendParameters = true;
                    dte.ParameterSeparator = "_";
                    dte.ParameterFormatter = "useLocaleParameterFormatter";
                }
            );

            // String api rendering
            builder.Conventions.AddMethodComponent(
                component: (c, cc) => MethodText(c.Method, cc),
                when: c => c.Method.DefaultOverload.ReturnType.Is<string>(),
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Content))
            );

            // None localized enums
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => EnumInline(c.Type, cc, requireLocalization: false),
                when: c => c.Type.Is<CacheKey>() || c.Type.Is<RowCount>()
            );

            // Route parameters sample
            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<RouteParameterSample>() && c.Method.DefaultOverload.ReturnsList(),
                where: cc => cc.Path.EndsWith(nameof(Page), nameof(RouteParameterSample), nameof(ContainerPage), nameof(ContainerPage.Contents), "*"),
                component: (c, cc) => MethodDataPanel(c.Method, cc)
            );

            builder.Conventions.AddTypeComponent(
                when: c => c.Type.Is<RouteParameterSample>(),
                where: cc => cc.Path.EndsWith(nameof(Page), nameof(RouteParameterSample)),
                component: (c, cc) => TypeContainerPage(c.Type, cc)
            );

            builder.Conventions.AddTypeComponentConfiguration<ContainerPage>(
                when: c => c.Type.Is<RouteParameterSample>(),
                where: cc => true,
                component: (container, c, cc) =>
                {
                    var contentContext = cc.Drill(nameof(ContainerPage), nameof(ContainerPage.Contents));

                    foreach (var method in c.Type.GetMembers().Methods)
                    {
                        var component = method.GetComponent(contentContext.Drill(container.Schema.Contents.Count));
                        if (component is null) { continue; }

                        container.Schema.Contents.Add(component);
                    }
                },
                order: int.MaxValue
            );

            // TODO - review this in form components
            // below this point is vibe coding
            builder.Conventions.AddTypeComponentConfiguration<ReportPage>(
                component: (rp, c, cc) =>
                {
                    var forms = new List<ReportPage.Tab.Content>();
                    var firstTab = rp.Schema.Tabs.First();

                    foreach (var method in c.Type.GetMembers().Methods.Having<ActionModelAttribute>())
                    {
                        var action = method.GetAction();
                        if (action.Method == HttpMethod.Get) { continue; }

                        forms.Add(method.GetRequiredSchema<ReportPage.Tab.Content>(
                            cc.Drill(nameof(ReportPage.Tabs), firstTab.Id, nameof(ReportPage.Tab.Contents), firstTab.Contents.Count + forms.Count)
                        ));
                    }

                    firstTab.Contents.InsertRange(0, forms);
                }
            );
            builder.Conventions.AddMethodSchemaConfiguration<ReportPage.Tab.Content>(
                when: c => !c.Method.Name.StartsWith("Get"),
                schema: rptc => rptc.Narrow = true
            );
            builder.Conventions.AddMethodComponentConfiguration<DataPanel>(
                when: c =>
                    c.Method.Name.StartsWith("Get") &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Methods.Having<ActionModelAttribute>().Any(m => !m.Name.StartsWith("Get")),
                component: dp =>
                {
                    dp.Schema.Content.Binding = "something-changed";
                }
            );
            builder.Conventions.AddMethodComponent(
                when: c => !c.Method.Name.StartsWith("Get"),
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Content)),
                component: c => C.VibeForm(options: vf =>
                {
                    var action = c.Method.GetAction();

                    vf.Label = c.Method.Name;
                    vf.Action.Path = action.GetRoute();
                    vf.Action.Method = action.Method.ToString().ToUpperInvariant();
                    vf.SubmitEventName = "something-changed";
                })
            );
            builder.Conventions.AddMethodComponentConfiguration<VibeForm>(
                component: (vf, c, cc) =>
                {
                    cc = cc.Drill(nameof(VibeForm));

                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        vf.Schema.Parameters.Add(ParameterParameter(parameter, cc.Drill(nameof(VibeForm.Parameters)), options: p =>
                        {
                            p.Required = !parameter.IsOptional;
                        }));
                    }
                }
            );
            builder.Conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.Is<string>(),
                component: c => C.InputText(c.Parameter.Name)
            );
            builder.Conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.Is<int>(),
                component: c => C.InputNumber(c.Parameter.Name)
            );
            // END OF TODO - review this in form components
        });

        configurator.ConfigureComponentExports(c =>
        {
            c.AddFromExtensions(typeof(C));
        });

        configurator.ConfigurePageDescriptors(pages =>
        {
            pages.Add(C.LoginPage("login", options: lp => lp.Layout = "modal"));
            pages.Add(C.RoutedPage("page/with/route/pageWithRoute", lp => lp.Layout = "default"));
        });
    }
}