using Baked.Architecture;
using Baked.Test.Caching;
using Baked.Theme;
using Baked.Theme.Admin;
using Baked.Ui;

using static Baked.Theme.Admin.Components;
using static Baked.Theme.Admin.DomainComponents;
using static Baked.Theme.Admin.DomainDatas;
using static Baked.Test.Theme.Custom.DomainComponents;
using static Baked.Test.Theme.Custom.Components;
using static Baked.Ui.Datas;

namespace Baked.Test.Theme.Custom;

public class CustomThemeFeature(IEnumerable<Func<Router, Baked.Theme.Route>> _routes)
    : AdminThemeFeature(_routes.Select(r => r(new())),
        _sideMenuOptions: sm => sm.Footer = LanguageSwitcher()
    )
{
    public override void Configure(LayerConfigurator configurator)
    {
        base.Configure(configurator);

        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // Custom theme CSV formatter settings
            builder.Conventions.AddMethodSchemaConvention<Baked.Theme.Admin.DataTable.Export>(
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
                component: (c, cc) => MethodString(c.Method, cc),
                whenMethod: c => c.Method.DefaultOverload.ReturnType.Is<string>(),
                whenComponent: c => c.Path.EndsWith(nameof(Baked.Theme.Admin.DataPanel), nameof(Baked.Theme.Admin.DataPanel.Content))
            );

            // None localized enums
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => EnumInline(c.Type, cc, requireLocalization: false),
                whenType: c => c.Type.Is<CacheKey>() || c.Type.Is<RowCount>()
            );

            #region Cache Samples Page Overrides

            builder.Conventions.AddTypeComponentConvention<ReportPage>(
                component: rp =>
                {
                    rp.Schema.Tabs[0].Contents[0].Narrow = true;
                    rp.Schema.Tabs[0].Contents[1].Narrow = true;
                },
                whenType: c => c.Type.Is<CacheSamples>()
            );

            #endregion

            #region Data Table Page Overrides

            // DataTable fine tuning
            builder.Conventions.AddMethodComponentConvention<Baked.Theme.Admin.DataTable>(
                component: dt =>
                {
                    dt.Schema.ScrollHeight = "500px";
                    dt.Schema.Paginator = null;
                    dt.Schema.Rows = null;
                },
                whenMethod: c => c.Type.Is<DataTable>()
            );
            builder.Conventions.AddMethodSchemaConvention<Baked.Theme.Admin.DataTable.Export>(
                schema: dte =>
                {
                    dte.ParameterFormatter = null;
                    dte.ParameterSeparator = null;
                },
                whenMethod: c => c.Type.Is<DataTable>()
            );
            builder.Conventions.AddMethodSchema(
                schema: () => DataTableVirtualScroller(options: dtvs => dtvs.ItemSize = 45),
                whenMethod: c => c.Type.Is<DataTable>()
            );

            #endregion

            #region Report Page Overrides

            // Tabs
            builder.Conventions.AddMethodConvention<TabAttribute>(
                apply: (tab, c) => tab.Name = "SingleValue",
                when: (_, c) => c.Type.Is<Report>() && c.Method.DefaultOverload.ReturnType.SkipTask().Is<string>()
            );
            builder.Conventions.AddMethodConvention<TabAttribute>(
                apply: (tab, c) => tab.Name = "DataTable",
                when: (_, c) => c.Type.Is<Report>() && c.Method.DefaultOverload.ReturnsList()
            );
            builder.Conventions.AddTypeComponent(
                component: () => Icon("pi-box"),
                whenType: c => c.Type.Is<Report>(),
                whenComponent: cc => cc.Path.EndsWith("SingleValue", nameof(Baked.Theme.Admin.ReportPage.Tab.Icon))
            );
            builder.Conventions.AddTypeComponent(
                component: () => Icon("pi-table"),
                whenType: c => c.Type.Is<Report>(),
                whenComponent: cc => cc.Path.EndsWith("DataTable", nameof(Baked.Theme.Admin.ReportPage.Tab.Icon))
            );

            // Allowing admin token for report api
            builder.Conventions.AddMethodSchemaConvention<RemoteData>(
                schema: rd => rd.Headers = Inline(new { Authorization = "token-admin-ui" }),
                whenMethod: c => c.Type.Is<Report>()
            );

            // Parameter overrides
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => EnumSelect(c.Parameter, cc),
                whenParameter: c => c.Type.Is<Report>() && c.Method.Name == nameof(Report.With) && !c.Parameter.IsOptional
            );
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => EnumSelect(c.Parameter, cc),
                whenParameter: c => c.Type.Is<Report>() && c.Method.Name == nameof(Report.GetFirst) && c.Parameter.Name == "count"
            );

            // Page overrides
            builder.Conventions.AddTypeComponentConvention<ReportPage>(
                component: rp =>
                {
                    rp.Schema.QueryParameters.Single(p => p.Name == "required").Default = null;
                    rp.Schema.Tabs[0].Contents[1].Narrow = true;
                    rp.Schema.Tabs[0].Contents[2].Narrow = true;
                    rp.Schema.Tabs[0].Contents[1].Component.Schema.As<DataPanel>().Collapsed = true;
                    rp.Schema.Tabs[0].Contents[2].Component.Schema.As<DataPanel>().Collapsed = true;
                    rp.Schema.Tabs[1].Contents[1].Component.Schema.As<DataPanel>().Collapsed = true;
                },
                whenType: c => c.Type.Is<Report>()
            );

            #endregion
        });

        configurator.ConfigureComponentExports(c =>
        {
            c.AddFromExtensions(typeof(Components));
        });

        configurator.ConfigurePageDescriptors(pages =>
        {
            pages.Add(LoginPage("login", options: lp => lp.Layout = "modal"));
            pages.Add(RoutedPage("page/with/route/pageWithRoute", lp => lp.Layout = "default"));
        });
    }
}