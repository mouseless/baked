using Baked.Architecture;
using Baked.Business;
using Baked.RestApi.Model;
using Baked.Ui;

namespace Baked.Ux.ActionsAreContents;

public class ActionsAreContentsUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.EditTypeComponent<SimplePage>(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Methods.Having<ApiAction>().Any(m => m.GetAction().Method == HttpMethod.Get),
                component: (sp, c, cc) =>
                {
                    cc = cc.Drill("simple-page", "contents");

                    foreach (var method in c.Type.GetMembers().Methods.Having<ApiAction>())
                    {
                        if (method.Has<Initializer>()) { continue; }
                        if (!method.TryGet<ApiAction>(out var action)) { continue; }
                        if (action.Method != HttpMethod.Get) { continue; }

                        var content = method.GenerateSchema<Content>(cc.Drill(sp.Schema.Contents.Count));
                        if (content is null) { continue; }

                        sp.Schema.Contents.Add(content);
                    }
                }
            );
            conventions.EditTypeComponent<TabbedPage>(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Methods.Having<ApiAction>().Any(m => m.GetAction().Method == HttpMethod.Get),
                component: (tp, c, cc) =>
                {
                    cc = cc.Drill("tabbed-page", "tabs");
                    var tabs = new Dictionary<string, Tab>();

                    var members = c.Type.GetMembers();
                    foreach (var method in members.Methods.Having<ApiAction>())
                    {
                        if (method.Has<Initializer>()) { continue; }

                        var action = method.Get<ApiAction>();
                        if (action.Method != HttpMethod.Get) { continue; }

                        if (!tabs.TryGetValue(method.TabName, out var tab))
                        {
                            tabs.Add(method.TabName, tab = members.GenerateRequiredSchema<Tab>(cc.Drill(method.TabName)));
                        }

                        var content = method.GenerateSchema<Content>(cc.Drill(method.TabName, "contents", tab.Contents.Count));
                        if (content is null) { continue; }

                        tab.Contents.Add(content);
                    }

                    tp.Schema.Tabs.AddRange(tabs.Values);
                },
                order: -10
            );
        });
    }
}