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
            builder.Conventions.AddTypeComponentConfiguration<TabbedPage>(
                component: (tp, c, cc) =>
                {
                    cc = cc.Drill(nameof(TabbedPage), nameof(TabbedPage.Tabs));
                    var tabs = new Dictionary<string, Tab>();

                    var members = c.Type.GetMembers();
                    foreach (var method in members.Methods.Having<TabNameAttribute>())
                    {
                        var tabName = method.Get<TabNameAttribute>();
                        var action = method.GetAction();
                        if (action.Method != HttpMethod.Get) { continue; }

                        if (!tabs.TryGetValue(tabName.Value, out var t))
                        {
                            tabs.Add(tabName.Value, t = TypeTab(c.Type, cc, tabName.Value));
                        }

                        t.Contents.Add(
                            method.GetRequiredSchema<Content>(
                                cc.Drill(tabName.Value, nameof(Tab.Contents), t.Contents.Count)
                            )
                        );
                    }

                    tp.Schema.Tabs.AddRange(tabs.Values);
                },
                when: c => c.Type.HasMembers(),
                order: -10
            );
            builder.Conventions.AddTypeComponentConfiguration<TabbedPage>(
               component: (tp, c, cc) =>
               {
                   if (tp.Schema.Tabs.Count <= 1) { return; }

                   var (_, l) = cc;

                   foreach (var tab in tp.Schema.Tabs)
                   {
                       tab.Title = l(tab.Id.Replace("-", "_").Titleize());
                   }
               }
            );
        });
    }
}