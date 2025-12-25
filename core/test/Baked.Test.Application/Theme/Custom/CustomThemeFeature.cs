using Baked.Architecture;
using Baked.Business;
using Baked.Test.Caching;
using Baked.Test.Ui;
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

            // Non-localized enums
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => EnumInline(c.Type, cc, requireLocalization: false),
                when: c => c.Type.Is<CacheKey>() || c.Type.Is<RowCount>()
            );

            // TODO move to UX
            // Row Actions
            builder.Conventions.AddMethodSchemaConfiguration<DataTable.Column>(
                when: c =>
                    c.Method.DefaultOverload.ReturnsList() &&
                    c.Method.DefaultOverload.ReturnType.TryGetElementType(out var itemType) &&
                    itemType.TryGetMembers(out var itemMembers),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions)),
                schema: (col, c, cc) =>
                {
                    var itemMembers = c.Method.DefaultOverload.ReturnType.GetElementType().GetMembers();
                    var actions = new List<IComponentDescriptor>();
                    foreach (var method in itemMembers.Methods.Having<ActionAttribute>())
                    {
                        if (method.Get<ActionAttribute>().HideInLists) { continue; }
                        if (method.Has<InitializerAttribute>()) { continue; }
                        if (method.GetAction().Method == HttpMethod.Get) { continue; }

                        var component = method.GetComponent(cc.Drill(method.Name));
                        if (component is null) { continue; }

                        actions.Add(component);
                    }

                    if (!actions.Any()) { return; }

                    col.Component = C.Container(options: c => c.Contents.AddRange(actions));
                }
            );
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                component: dt =>
                {
                    if (dt.Schema.Actions is null) { return; }
                    if (dt.Schema.Actions.Component is not ComponentDescriptor<Container> container) { return; }

                    foreach (var component in container.Schema.Contents)
                    {
                        if (component.Action is not RemoteAction remote) { continue; }
                        if (remote.PostAction is not PublishAction publish) { continue; }
                        if (publish.Event is null) { continue; }

                        dt.ReloadOn(publish.Event);
                    }
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
            pages.Add(C.RoutedPage("parent/[id]", lp => lp.Layout = "default"));
            pages.Add(C.RoutedPage("parent/[parentId]/children/[childId]", lp => lp.Layout = "default"));
        });
    }
}