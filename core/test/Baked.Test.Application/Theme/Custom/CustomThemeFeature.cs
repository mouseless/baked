using Baked.Architecture;
using Baked.Test.Caching;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;

using static Baked.Test.Theme.Custom.DomainComponents;
using static Baked.Theme.Default.DomainComponents;
using static Baked.Theme.Default.DomainDatas;
using static Baked.Ui.Datas;

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
            builder.Conventions.AddMethodSchemaConfiguration<Baked.Ui.DataTable.Export>(
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

            #region Cache Samples Page Overrides

            builder.Conventions.AddTypeComponentConfiguration<ReportPage>(
                component: rp =>
                {
                    rp.Schema.Tabs[0].Contents[0].Narrow = true;
                    rp.Schema.Tabs[0].Contents[1].Narrow = true;
                },
                when: c => c.Type.Is<CacheSamples>()
            );

            #endregion

            #region Data Table Page Overrides

            // DataTable fine tuning
            builder.Conventions.AddMethodComponentConfiguration<Baked.Ui.DataTable>(
                component: dt =>
                {
                    dt.Schema.ScrollHeight = "500px";
                    dt.Schema.Paginator = null;
                    dt.Schema.Rows = null;
                },
                when: c => c.Type.Is<DataTable>()
            );
            builder.Conventions.AddMethodSchemaConfiguration<Baked.Ui.DataTable.Export>(
                schema: dte =>
                {
                    dte.ParameterFormatter = null;
                    dte.ParameterSeparator = null;
                },
                when: c => c.Type.Is<DataTable>()
            );
            builder.Conventions.AddMethodSchema(
                schema: () => B.DataTableVirtualScroller(options: dtvs => dtvs.ItemSize = 45),
                when: c => c.Type.Is<DataTable>()
            );

            #endregion

            #region Report Page Overrides

            // Tabs
            builder.Conventions.AddMethodAttributeConfiguration<TabNameAttribute>(
                attribute: (tabName, c) => tabName.Value = "SingleValue",
                when: c => c.Type.Is<Report>() && c.Method.DefaultOverload.ReturnType.SkipTask().Is<string>()
            );
            builder.Conventions.AddMethodAttributeConfiguration<TabNameAttribute>(
                attribute: (tabName, c) => tabName.Value = "DataTable",
                when: c => c.Type.Is<Report>() && c.Method.DefaultOverload.ReturnsList()
            );
            builder.Conventions.AddTypeComponent(
                component: () => B.Icon("pi-box"),
                when: c => c.Type.Is<Report>(),
                where: cc => cc.Path.EndsWith("SingleValue", nameof(ReportPage.Tab.Icon))
            );
            builder.Conventions.AddTypeComponent(
                component: () => B.Icon("pi-table"),
                when: c => c.Type.Is<Report>(),
                where: cc => cc.Path.EndsWith("DataTable", nameof(ReportPage.Tab.Icon))
            );

            // Allowing admin token for report api
            builder.Conventions.AddMethodSchemaConfiguration<RemoteData>(
                schema: rd => rd.Headers = Inline(new { Authorization = "token-admin-ui" }),
                when: c => c.Type.Is<Report>()
            );

            // Parameter overrides
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => EnumSelect(c.Parameter, cc),
                when: c => c.Type.Is<Report>() && c.Method.Name == nameof(Report.With) && !c.Parameter.IsOptional
            );
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => EnumSelect(c.Parameter, cc),
                when: c => c.Type.Is<Report>() && c.Method.Name == nameof(Report.GetFirst) && c.Parameter.Name == "count"
            );

            // Page overrides
            builder.Conventions.AddTypeComponentConfiguration<ReportPage>(
                component: rp =>
                {
                    rp.Schema.QueryParameters.Single(p => p.Name == "required").Default = null;
                    rp.Schema.Tabs[0].Contents[1].Narrow = true;
                    rp.Schema.Tabs[0].Contents[2].Narrow = true;
                    rp.Schema.Tabs[0].Contents[1].Component.Schema.As<DataPanel>().Collapsed = true;
                    rp.Schema.Tabs[0].Contents[2].Component.Schema.As<DataPanel>().Collapsed = true;
                    rp.Schema.Tabs[1].Contents[1].Component.Schema.As<DataPanel>().Collapsed = true;
                },
                when: c => c.Type.Is<Report>()
            );

            #endregion
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