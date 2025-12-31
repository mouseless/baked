using Baked.Architecture;
using Baked.Business;
using Baked.RestApi.Model;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Default.DomainComponents;

namespace Baked.Ux.ActionsAreContents;

public class ActionsAreContentsUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeComponentConfiguration<SimplePage>(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Methods.Having<ActionModelAttribute>().Any(m => m.GetAction().Method == HttpMethod.Get),
                component: (sp, c, cc) =>
                {
                    cc = cc.Drill(nameof(SimplePage), nameof(SimplePage.Contents));

                    foreach (var method in c.Type.GetMembers().Methods.Having<ActionModelAttribute>())
                    {
                        if (method.Has<InitializerAttribute>()) { continue; }
                        if (!method.TryGet<ActionModelAttribute>(out var action)) { continue; }
                        if (action.Method != HttpMethod.Get) { continue; }

                        var content = method.GetSchema<Content>(cc.Drill(sp.Schema.Contents.Count));
                        if (content is null) { continue; }

                        sp.Schema.Contents.Add(content);
                    }
                }
            );
            builder.Conventions.AddTypeComponentConfiguration<TabbedPage>(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Methods.Having<ActionModelAttribute>().Any(m => m.GetAction().Method == HttpMethod.Get),
                component: (tp, c, cc) =>
                {
                    cc = cc.Drill(nameof(TabbedPage), nameof(TabbedPage.Tabs));
                    var tabs = new Dictionary<string, Tab>();

                    var members = c.Type.GetMembers();
                    foreach (var method in members.Methods.Having<TabNameAttribute>())
                    {
                        if (method.Has<InitializerAttribute>()) { continue; }
                        if (!method.TryGet<ActionModelAttribute>(out var action)) { continue; }
                        if (action.Method != HttpMethod.Get) { continue; }

                        var tabName = method.Get<TabNameAttribute>();
                        if (!tabs.TryGetValue(tabName.Value, out var t))
                        {
                            tabs.Add(tabName.Value, t = TypeTab(c.Type, cc, tabName.Value));
                        }

                        var content = method.GetSchema<Content>(cc.Drill(tabName.Value, nameof(Tab.Contents), t.Contents.Count));
                        if (content is null) { continue; }

                        t.Contents.Add(content);
                    }

                    tp.Schema.Tabs.AddRange(tabs.Values);
                },
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