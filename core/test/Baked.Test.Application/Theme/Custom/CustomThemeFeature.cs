using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Test.Caching;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;

using static Baked.Test.Theme.Custom.DomainComponents;
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
            builder.Conventions.AddMethodComponent(
                when: c => !c.Method.Name.StartsWith("Get"),
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Content)),
                component: c => C.VibeForm(options: vf =>
                {
                    var action = c.Method.GetAction();

                    vf.Label = c.Method.Name;
                    vf.Endpoint.Path = action.GetRoute();
                    vf.Endpoint.Method = action.Method.ToString().ToUpperInvariant();
                    vf.SubmitEventName = "something-changed";
                })
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