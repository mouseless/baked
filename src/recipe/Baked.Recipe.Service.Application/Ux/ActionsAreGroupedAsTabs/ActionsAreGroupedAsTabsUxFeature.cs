using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Theme;
using Baked.Theme.Admin;
using Humanizer;

using static Baked.Theme.Admin.DomainComponents;

namespace Baked.Ux.ActionsAreGroupedAsTabs;

public class ActionsAreGroupedAsTabsUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetMethodMetadata(
                attribute: _ => new TabAttribute(),
                when: c => c.Method.Has<ActionModelAttribute>(),
                order: int.MaxValue - 5
            );
            builder.Conventions.AddTypeComponentConfiguration<ReportPage>(
                component: (rp, c, cc) =>
                {
                    cc = cc.Drill(nameof(ReportPage.Tabs));
                    var tabs = new Dictionary<string, ReportPage.Tab>();

                    var members = c.Type.GetMembers();
                    foreach (var method in members.Methods.Having<ActionModelAttribute>())
                    {
                        var action = method.GetAction();
                        if (action.Method != HttpMethod.Get) { continue; }
                        if (!method.TryGet<TabAttribute>(out var tab)) { continue; }

                        if (!tabs.TryGetValue(tab.Name, out var t))
                        {
                            tabs.Add(tab.Name, t = TypeReportPageTab(c.Type, cc.Drill(tab.Name), tab.Name));
                        }

                        t.Contents.Add(
                            method.GetRequiredSchema<ReportPage.Tab.Content>(
                                cc.Drill(tab.Name, nameof(ReportPage.Tab.Contents), t.Contents.Count)
                            )
                        );
                    }

                    rp.Schema.Tabs.AddRange(tabs.Values);
                },
                whenType: c => c.Type.HasMembers()
            );
            builder.Conventions.AddTypeComponentConfiguration<ReportPage>(
               component: (rp, c, cc) =>
               {
                   var (_, l) = cc;

                   foreach (var rpt in rp.Schema.Tabs)
                   {
                       if (rpt.Id == "default") { continue; }

                       rpt.Title = l(rpt.Id.Replace("-", "_").Titleize());
                   }
               }
            );
            builder.Conventions.AddMethodSchema(
                schema: (c, cc) => MethodReportPageTabContent(c.Method, cc),
                whenMethod: c => c.Method.Has<ActionModelAttribute>()
            );
        });
    }
}