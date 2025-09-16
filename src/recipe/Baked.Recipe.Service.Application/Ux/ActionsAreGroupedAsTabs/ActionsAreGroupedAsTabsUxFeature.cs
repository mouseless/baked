using Baked.Architecture;
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
            builder.Conventions.AddTypeComponentConfiguration<ReportPage>(
                component: (rp, c, cc) =>
                {
                    cc = cc.Drill(nameof(ReportPage.Tabs));
                    var tabs = new Dictionary<string, ReportPage.Tab>();

                    var members = c.Type.GetMembers();
                    foreach (var method in members.Methods.Having<TabAttribute>())
                    {
                        var tab = method.Get<TabAttribute>();
                        var action = method.GetAction();
                        if (action.Method != HttpMethod.Get) { continue; }

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
                   if (rp.Schema.Tabs.Count <= 1) { return; }

                   var (_, l) = cc;

                   foreach (var rpt in rp.Schema.Tabs)
                   {
                       rpt.Title = l(rpt.Id.Replace("-", "_").Titleize());
                   }
               }
            );
        });
    }
}