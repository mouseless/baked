using Baked.Architecture;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Default.DomainComponents;

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
                    cc = cc.Drill(nameof(ReportPage), nameof(ReportPage.Tabs));
                    var tabs = new Dictionary<string, ReportPage.Tab>();

                    var members = c.Type.GetMembers();
                    foreach (var method in members.Methods.Having<TabNameAttribute>())
                    {
                        var tabName = method.Get<TabNameAttribute>();
                        var action = method.GetAction();
                        if (action.Method != HttpMethod.Get) { continue; }

                        if (!tabs.TryGetValue(tabName.Value, out var t))
                        {
                            tabs.Add(tabName.Value, t = TypeReportPageTab(c.Type, cc, tabName.Value));
                        }

                        t.Contents.Add(
                            method.GetRequiredSchema<ReportPage.Tab.Content>(
                                cc.Drill(tabName.Value, nameof(ReportPage.Tab.Contents), t.Contents.Count)
                            )
                        );
                    }

                    rp.Schema.Tabs.AddRange(tabs.Values);
                },
                whenType: c => c.Type.HasMembers(),
                order: -10
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