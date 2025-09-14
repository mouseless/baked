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
            // NOTE Custom theme CSV formatter settings
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

            // TODO Move this to admin feature... Why not!
            builder.Conventions.AddMethodComponent(
                component: (c, cc) => MethodString(c.Method, cc),
                whenMethod: c => c.Method.DefaultOverload.ReturnType.Is<string>(),
                whenComponent: c => c.Path.EndsWith(nameof(Baked.Theme.Admin.DataPanel), nameof(Baked.Theme.Admin.DataPanel.Content))
            );

            // NOTE None localized enums
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => EnumInline(c.Type, cc, requireLocalization: false),
                whenType: c => c.Type.Is<CacheKey>() || c.Type.Is<RowCount>()
            );

            #region Cache Samples Page Overrides

            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => TypeReportPageTab(c.Type, cc, "Default"),
                whenType: c => c.Type.Is<CacheSamples>()
            );
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => EnumSelect(c.Parameter, cc),
                whenParameter: c => c.Type.Is<CacheSamples>()
            );
            builder.Conventions.AddParameterComponentConvention<Select>(
                component: (s, c, cc) => s.Schema.LocalizeLabel = null,
                whenParameter: c => c.Type.Is<CacheSamples>()
            );
            builder.Conventions.AddTypeComponentConvention<ReportPage>(
                component: rp =>
                {
                    var defaultTab = rp.Schema.Tabs.Single(t => t.Id == "default");
                    defaultTab.Contents[0].Narrow = true;
                    defaultTab.Contents[1].Narrow = true;
                },
                whenType: c => c.Type.Is<CacheSamples>()
            );

            #endregion

            #region Data Table Page Overrides

            // NOTE Tabs
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => TypeReportPageTab(c.Type, cc, "Default"),
                whenType: c => c.Type.Is<DataTable>()
            );

            // NOTE DataTable fine tuning
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

            // NOTE Tabs
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => TypeReportPageTab(c.Type, cc, "SingleValue"),
                whenType: c => c.Type.Is<Report>()
            );
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => TypeReportPageTab(c.Type, cc, "DataTable"),
                whenType: c => c.Type.Is<Report>()
            );
            builder.Conventions.AddMethodConvention<TabAttribute>(
                apply: (tab, c) => tab.Name = c.Method.DefaultOverload.ReturnType.Is<string>() ? "SingleValue" : "DataTable",
                when: (_, c) => c.Type.Is<Report>()
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

            // NOTE Parameter tweaks
            builder.Conventions.AddParameterSchemaConvention<Parameter>(
                schema: p => p.Default = null,
                whenParameter: c => c.Type.Is<Report>() && c.Method.Name == nameof(Report.With) && c.Parameter.Name == "required"
            );
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => EnumSelect(c.Parameter, cc),
                whenParameter: c => c.Type.Is<Report>() && c.Method.Name == nameof(Report.With) && !c.Parameter.IsOptional
            );
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => EnumSelect(c.Parameter, cc),
                whenParameter: c => c.Type.Is<Report>() && c.Method.Name == nameof(Report.GetFirst) && c.Parameter.Name == "count"
            );

            // NOTE Panel tweaks
            builder.Conventions.AddMethodSchemaConvention<ReportPage.Tab.Content>(
                schema: rtc => rtc.Narrow = true,
                whenMethod: c =>
                    c.Type.Is<Report>() &&
                    (c.Method.Name == nameof(Report.GetLeft) || c.Method.Name == nameof(Report.GetRight))
            );
            builder.Conventions.AddMethodComponentConvention<DataPanel>(
                component: dp => dp.Schema.Collapsed = true,
                whenMethod: c =>
                    c.Type.Is<Report>() &&
                    (
                        c.Method.Name == nameof(Report.GetLeft) ||
                        c.Method.Name == nameof(Report.GetRight) ||
                        c.Method.Name == nameof(Report.GetSecond)
                    )
            );
            builder.Conventions.AddMethodComponentConvention<DataPanel>(
                component: dp => dp.Schema.Collapsed = false,
                whenMethod: c =>
                    c.Type.Is<Report>() &&
                    c.Method.Name == nameof(Report.GetWide)
            );

            // NOTE Remove export from `GetSecond`
            // TODO remove this exception later
            builder.Conventions.RemoveMethodMetadata<DescriptorBuilderAttribute<Baked.Theme.Admin.DataTable.Export>>(
                when: c =>
                    c.Type.Is<Report>() &&
                    c.Method.Name == nameof(Report.GetSecond),
                order: int.MaxValue
            );
            builder.Conventions.AddMethodComponentConvention<Baked.Theme.Admin.DataTable>(
                component: dt =>
                {
                    foreach (var column in dt.Schema.Columns)
                    {
                        column.Exportable = null;
                    }
                },
                whenMethod: c =>
                    c.Type.Is<Report>() &&
                    c.Method.Name == nameof(Report.GetSecond)
            );

            builder.Conventions.AddMethodSchemaConvention<RemoteData>(
                schema: rd => rd.Headers = Inline(new { Authorization = "token-admin-ui" }),
                whenMethod: c => c.Type.Is<Report>()
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