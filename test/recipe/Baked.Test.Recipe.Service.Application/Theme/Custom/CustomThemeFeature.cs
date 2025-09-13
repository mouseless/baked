using Baked.Architecture;
using Baked.Theme;
using Baked.Theme.Admin;
using Baked.Ui;

using static Baked.Theme.Admin.Components;
using static Baked.Theme.Admin.DomainComponents;
using static Baked.Test.Theme.Custom.DomainComponents;
using static Baked.Test.Theme.Custom.Components;
using static Baked.Ui.Datas;

using ReportPageC = Baked.Theme.Admin.ReportPage;

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
            builder.Conventions.AddTypeComponent(
                component: (c, cc) => TypeReportPage(c.Type, cc),
                whenType: c => c.Type.Is<Report>(),
                whenComponent: cc => cc.Path.Is(nameof(Page))
            );
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => TypeReportPageTab(c.Type, cc, "SingleValue"),
                whenType: c => c.Type.Is<Report>()
            );
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => TypeReportPageTab(c.Type, cc, "DataTable"),
                whenType: c => c.Type.Is<Report>()
            );
            builder.Conventions.AddTypeComponent(
                component: () => Icon("pi-box"),
                whenType: c => c.Type.Is<Report>(),
                whenComponent: cc => cc.Path.EndsWith("SingleValue", nameof(ReportPageC.Tab.Icon))
            );
            builder.Conventions.AddTypeComponent(
                component: () => Icon("pi-table"),
                whenType: c => c.Type.Is<Report>(),
                whenComponent: cc => cc.Path.EndsWith("DataTable", nameof(ReportPageC.Tab.Icon))
            );
            builder.Conventions.AddParameterSchemaConvention<Parameter>(
                schema: p => p.Default = null,
                whenParameter: c => c.Type.Is<Report>() && c.Method.Name == nameof(Report.With) && c.Parameter.Name == "required"
            );
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => EnumSelect(c.Parameter, cc),
                whenParameter: c => c.Type.Is<Report>() && c.Method.Name == nameof(Report.With) && !c.Parameter.IsOptional
            );
            builder.Conventions.AddMethodConvention<TabAttribute>(
                apply: (tab, c) => tab.Name = c.Method.DefaultOverload.ReturnType.Is<string>() ? "SingleValue" : "DataTable",
                when: (_, c) => c.Type.Is<Report>()
            );
            builder.Conventions.AddMethodSchemaConvention<ReportPageC.Tab.Content>(
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
            builder.Conventions.AddMethodComponent(
                component: (c, cc) => MethodString(c.Method, cc),
                whenMethod: c => c.Method.DefaultOverload.ReturnType.Is<string>(),
                whenComponent: c => c.Path.Matches(Regexes.AnyDataPanelContent)
            );
            builder.Conventions.AddMethodSchemaConvention<RemoteData>(
                schema: rd => rd.Headers = Inline(new { Authorization = "token-admin-ui" }),
                whenMethod: c => c.Type.Is<Report>()
            );
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