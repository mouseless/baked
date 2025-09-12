using Baked.Business;
using Baked.Test.Caching;
using Baked.Test.Theme;
using Baked.Test.Theme.Custom;
using Baked.Theme;

using static Baked.Theme.Admin.Components;
using static Baked.Test.Theme.Custom.DomainComponents;
using static Baked.Ui.Datas;

namespace Baked;

public static class CustomThemeExtensions
{
    public static CustomThemeFeature Custom(this ThemeConfigurator _) =>
        new(
        [
            r => r.Index() with { Page = p => p.Described(d => d.Menu()) },
            r => r.Root("/cache", "Cache", "pi pi-database") with { Page = p => p.Described(d => d.Cache()), Description = "Showcases the cache behavior" },
            r => r.Root("/data-table", "Data Table", "pi pi-table") with { Page = p => p.Described(d => d.DataTable()), Description = "Showcase DataTable component with scrollable and footer options" },
            // r => r.Root("/report", "Report", "pi pi-file") with { Page = p => p.Generated(d => d.From<Report>()), Description = "Showcases a report layout with tabs and data panels"},
            r => r.Root("/report", "Report", "pi pi-file") with { Page = p => p.Described(d => d.Report()), Description = "Showcases a report layout with tabs and data panels"},
            r => r.Root("/specs", "Specs", "pi pi-list-check") with { Page = p => p.Described(d => d.Menu()), Description = "All UI Specs are listed here" },

            // Behavior
            r => r.Child("/specs/bake", "Bake", "/specs") with { Icon = "pi pi-microchip", Description = "The core component that renders a dynamic component using given descriptor", Section = "Behavior" },
            r => r.Child("/specs/custom-css", "Custom CSS", "/specs") with { Icon = "pi pi-microchip", Description = "Allow custom configuration to define custom css and more", Section = "Behavior" },
            r => r.Child("/specs/parameters", "Parameters", "/specs") with { Icon = "pi pi-microchip", Description = "Manage parameters through emits", Section = "Behavior" },
            r => r.Child("/specs/query-parameters", "Query Parameters", "/specs") with { Icon = "pi pi-microchip", Description = "Sync and manage parameters in query string", Section = "Behavior" },
            r => r.Child("/specs/toast", "Toast", "/specs") with { Icon = "pi pi-microchip", Description = "Render alert messages", Section = "Behavior" },

            // Display
            r => r.Child("/specs/card-link", "Card Link", "/specs") with { Icon = "pi pi-microchip", Description = "Renders a link as a big card-like button", Section = "Display" },
            r => r.Child("/specs/data-table", "Data Table", "/specs") with { Icon = "pi pi-microchip", Description = "View list data in a table", Section = "Display" },
            r => r.Child("/specs/nav-link", "Nav Link", "/specs") with { Icon = "pi pi-microchip", Description = "A component to give a link to a domain object", Section = "Display" },
            r => r.Child("/specs/icon", "Icon", "/specs") with { Icon = "pi pi-microchip", Description = "Displays built-in icons", Section = "Display" },
            r => r.Child("/specs/message", "Message", "/specs") with { Icon = "pi pi-microchip", Description = "A component to display message", Section = "Display" },
            r => r.Child("/specs/money", "Money", "/specs") with { Icon = "pi pi-microchip", Description = "Shortens and renders money values with the full value shown as tooltip", Section = "Display" },
            r => r.Child("/specs/number", "Number", "/specs") with { Icon = "pi pi-microchip", Description = "Shortens and renders numbers with the full value shown as tooltip", Section = "Display" },
            r => r.Child("/specs/rate", "Rate", "/specs") with { Icon = "pi pi-microchip", Description = "Render rate values as percentage", Section = "Display" },
            r => r.Child("/specs/string", "String", "/specs") with { Icon = "pi pi-microchip", Description = "Render string values", Section = "Display" },

            // Input
            r => r.Child("/specs/language-switcher", "Language Switcher", "/specs") with { Icon = "pi pi-microchip", Description = "Allow change site language", Section = "Input" },
            r => r.Child("/specs/select", "Select", "/specs") with { Icon = "pi pi-microchip", Description = "Allow select from given options using drow down", Section = "Input" },
            r => r.Child("/specs/select-button", "Select Button", "/specs") with { Icon = "pi pi-microchip", Description = "Allow select from given options using buttons", Section = "Input" },

            // Layout
            r => r.Child("/specs/data-panel", "Data Panel", "/specs") with { Icon = "pi pi-microchip", Description = "Lazy load and view a data within a panel", Section = "Layout" },
            r => r.Child("/specs/header", "Header", "/specs") with { Icon = "pi pi-microchip", Description = "Renders a breadcrumb", Section = "Layout" },
            r => r.Child("/specs/page-title", "Page Title", "/specs") with { Icon = "pi pi-microchip", Description = "Render page title, desc and actions", Section = "Layout" },
            r => r.Child("/specs/side-menu", "Side Menu", "/specs") with { Icon = "pi pi-microchip", Description = "Renders application menu", Section = "Layout" },

            // Page
            r => r.Child("/specs/error-page", "Error Page", "/specs") with { Icon = "pi pi-microchip", Description = "Display errors in full page", Section = "Page" },
            r => r.Child("/specs/menu-page", "Menu Page", "/specs") with { Icon = "pi pi-microchip", Description = "Render navigation pages", Section = "Page" },
            r => r.Child("/specs/report-page", "Report Page", "/specs") with { Icon = "pi pi-microchip", Description = "Render report pages", Section = "Page" },

            // Plugins
            r => r.Child("/specs/auth", "Auth", "/specs") with { Icon = "pi pi-microchip", Description = "Authorized routing and client", Section = "Plugins" },
            r => r.Child("/specs/cache", "Cache", "/specs") with { Icon = "pi pi-microchip", Description = "Caches api responses in local storage", Section = "Plugins" },
            r => r.Child("/specs/locale", "Locale", "/specs") with { Icon = "pi pi-microchip", Description = "Allow locale customization and language support", Section = "Plugins" },
            r => r.Child("/specs/error-handling", "Error Handling", "/specs") with { Icon = "pi pi-microchip", Description = "Handling errors", Section = "Plugins" },
        ]);

    public static PageBuilder Cache(this Page.Describer _) =>
        context =>
        {
            var (domain, l) = context;
            var headers = Inline(new { Authorization = "token-admin-ui" });

            var cacheSamples = domain.Types[typeof(CacheSamples)].GetMembers();
            var initializer = cacheSamples.Methods.Having<InitializerAttribute>().First().DefaultOverload;
            var getScoped = cacheSamples.Methods[nameof(CacheSamples.GetScoped)];
            var getApplication = cacheSamples.Methods[nameof(CacheSamples.GetApplication)];

            return ReportPage("cache", PageTitle("Cache", options: pt => pt.Description = l("Showcases the cache behavior")),
                options: rp =>
                {
                    rp.QueryParameters.Add(
                        EnumSelectParameter(initializer.Parameters["parameter"], context.Drill("Parameters", "Parameter"),
                            requireLocalization: false
                        )
                    );
                    rp.Tabs.Add(
                        ReportPageTab("default",
                            options: rpt =>
                            {
                                rpt.Contents.AddRange(
                                [
                                    ReportPageTabContent(
                                        component: DataPanel(l(getScoped.Name),
                                            content: ActionString(getScoped)
                                        ),
                                        options: rptc => rptc.Narrow = true
                                    ),
                                    ReportPageTabContent(
                                        component: DataPanel(l(getApplication.Name),
                                            content: ActionString(getApplication)
                                        ),
                                        options: rptc => rptc.Narrow = true
                                    )
                                ]);
                            }
                        )
                    );
                }
            );
        };

    public static PageBuilder DataTable(this Page.Describer _) =>
        context =>
        {
            var (domain, l) = context;
            var dataTable = domain.Types[typeof(DataTable)].GetMembers();
            var getTableDataWithFooter = dataTable.Methods[nameof(Test.Theme.DataTable.GetTableDataWithFooter)];

            return ReportPage("data-table", PageTitle(l("Data Table Demo")), options: rp =>
            {
                rp.Tabs.AddRange(
                [
                    ReportPageTab("default", options: rpt =>
                    {
                        rpt.Contents.AddRange(
                        [
                            ReportPageTabContent(
                                DataPanel(l("Data Panel"),
                                    content: TableWithFooterActionDataTable(getTableDataWithFooter, context.Drill("Tabs", "Default", "Contents", 0)),
                                    options: dp => dp.Parameters.Add(
                                        EnumSelectParameter(getTableDataWithFooter.DefaultOverload.Parameters["count"], context.Drill("Parameters", "count"),
                                            requireLocalization: false
                                        )
                                    )
                                )
                            )
                        ]);
                    })
                ]);
            });
        };

    public static PageBuilder Report(this Page.Describer _) =>
        context =>
        {
            var (domain, l) = context;
            var headers = Inline(new { Authorization = "token-admin-ui" });

            var report = domain.Types[typeof(Report)].GetMembers();
            var initializer = report.Methods.Having<InitializerAttribute>().First().DefaultOverload;
            var wide = report.Methods[nameof(Test.Theme.Report.GetWide)];
            var left = report.Methods[nameof(Test.Theme.Report.GetLeft)];
            var right = report.Methods[nameof(Test.Theme.Report.GetRight)];
            var first = report.Methods[nameof(Test.Theme.Report.GetFirst)];
            var second = report.Methods[nameof(Test.Theme.Report.GetSecond)];

            return ReportPage("report", PageTitle(l("Report"), options: pt => pt.Description = l("Showcases a report layout with tabs and data panels")), options: rp =>
            {
                rp.QueryParameters.AddRange(
                [
                    EnumSelectParameter(initializer.Parameters["requiredWithDefault"], context.Drill("Parameters", "requiredWithDefault")),
                    EnumSelectParameter(initializer.Parameters["required"], context.Drill("Parameters", "required"), options: p => p.Default = null),
                    EnumSelectButtonParameter(initializer.Parameters["optional"], context.Drill("Parameters", "optional"))
                ]);
                rp.Tabs.AddRange(
                [
                    ReportPageTab("single-value", options: rpt =>
                    {
                        rpt.Title = l("Single Value");
                        rpt.Icon = Icon("pi-box");
                        rpt.Contents.AddRange(
                        [
                            ReportPageTabContent(
                                component: DataPanel(l(wide.Name),
                                    content: ActionString(wide, dataOptions: rd => rd.Headers = headers),
                                    options: dp => dp.Collapsed = false
                                )
                            ),
                            ReportPageTabContent(
                                component: DataPanel(l(left.Name),
                                    content: ActionString(left, dataOptions: rd => rd.Headers = headers),
                                    options: dp => dp.Collapsed = true
                                ),
                                options: rptc => rptc.Narrow = true
                            ),
                            ReportPageTabContent(
                                component: DataPanel(l(right.Name),
                                    content: ActionString(right, dataOptions: rd => rd.Headers = headers),
                                    options: dp => dp.Collapsed = true
                                ),
                                options: rptc => rptc.Narrow = true
                            )
                        ]);
                    }),
                    ReportPageTab("data-table", options: rpt =>
                    {
                        rpt.Title = l("Data Table");
                        rpt.Icon = Icon("pi-table");
                        rpt.Contents.AddRange(
                        [
                            ReportPageTabContent(
                                component: DataPanel(l(first.Name),
                                    content: ReportRowListActionDataTable(first, context.Drill("Tabs", "DataTable", "Contents", 0, "Content"),
                                        dataOptions: rd => rd.Headers = headers
                                    ),
                                    options: dp => dp.Parameters.Add(
                                        EnumSelectParameter(first.DefaultOverload.Parameters["count"], context.Drill("Tabs", "DataTable", "Contents", 0, "Parameters", "count"),
                                            selectOptions: s => s.ShowClear = null
                                        )
                                    )
                                )
                            ),
                            ReportPageTabContent(
                                component: DataPanel(l(second.Name),
                                    content: ReportRowListActionDataTable(second, context.Drill("Tabs", "DataTable", "Contents", 1, "content"),
                                        exportable: false,
                                        dataOptions: rd => rd.Headers = headers
                                    ),
                                    options: dp =>
                                    {
                                        dp.Parameters.Add(
                                            EnumSelectButtonParameter(second.DefaultOverload.Parameters["count"], context.Drill("Tabs", "DataTable", "Contents", 1, "Parameters", "count"),
                                                selectButtonOptions: sb => sb.AllowEmpty = null
                                            )
                                        );
                                        dp.Collapsed = true;
                                    }
                                )
                            )
                        ]);
                    })
                ]);
            });
        };
}