using Baked.Architecture;
using Baked.Test.Caching;
using Baked.Test.Orm;
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

            // Custom routes
            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.Is<Parent>(),
                attribute: c => new RouteAttribute("/parents/{id}")
                {
                    Params = new() { { "id", c.Type.GetMembers().Properties[nameof(Parent.Id)].Get<DataAttribute>().Prop } }
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
            pages.Add(C.RoutedPage("first/[id]", lp => lp.Layout = "default"));
            pages.Add(C.RoutedPage("first/[firstId]/second/[secondId]", lp => lp.Layout = "default"));
        });
    }
}