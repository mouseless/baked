using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Test.Caching;
using Baked.Theme.Admin;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Admin.Components;
using static Baked.Ui.Datas;

namespace Baked.Test.Theme.Custom;

public class CustomThemeFeature : AdminThemeFeature
{
    readonly List<Spec> _specs =
    [
        new("Behavior",
        [
            new("Bake", "The core component that renders a dynamic component using given descriptor"),
            new("Custom CSS", "Allow custom configuration to define custom css and more"),
            new("Parameters", "Manage parameters through emits"),
            new("Query Parameters", "Sync and manage parameters in query string"),
            new("Toast", "Render alert messages")
        ]),
        new("Display",
        [
            new("Card Link", "Renders a link as a big card-like button"),
            new("Data Table", "View list data in a table"),
            new("Nav Link", "A component to give a link to a domain object"),
            new("Icon", "Displays built-in icons"),
            new("Message", "A component to display message"),
            new("Money", "Shortens and renders money values with the full value shown as tooltip"),
            new("Number", "Shortens and renders numbers with the full value shown as tooltip"),
            new("Rate", "Render rate values as percentage"),
            new("String", "Render string values")
        ]),
        new("Input",
        [
            new("Language Switcher", "Allow change site language"),
            new("Select", "Allow select from given options using drow down"),
            new("Select Button", "Allow select from given options using buttons")
        ]),
        new("Layout",
        [
            new("Data Panel", "Lazy load and view a data within a panel"),
            new("Header", "Renders a breadcrumb"),
            new("Page Title", "Render page title, desc and actions"),
            new("Side Menu", "Renders application menu")
        ]),
        new("Page",
        [
            new("Error Page", "Display errors in full page"),
            new("Menu Page", "Render navigation pages"),
            new("Report Page", "Render report pages")
        ]),
        new("Plugins",
        [
            new ("Auth", "Authorized routing and client"),
            new ("Cache", "Caches api responses in local storage"),
            new ("Locale", "Allow locale customization and language support"),
            new ("Error Handling", "Handling errors"),
        ]),
    ];

    public override void Configure(LayerConfigurator configurator)
    {
        base.Configure(configurator);

        configurator.ConfigureComponentExports(c =>
        {
            c.AddFromExtensions(typeof(Components));
        });

        configurator.ConfigureAppDescriptor(app =>
        {
            configurator.UsingLocalization(l =>
            {
                app.Error = ErrorPage(
                    options: ep =>
                    {
                        ep.SafeLinks.AddRange(
                        [
                            CardLink("/", l("Home"), options: cl => cl.Icon = "pi pi-home"),
                            CardLink("/cache", l("Cache"), options: cl => cl.Icon = "pi pi-database"),
                            CardLink("/data-table", l("Data Table"), options: cl => cl.Icon = "pi pi-table"),
                            CardLink("/report", l("Report"), options: cl => cl.Icon =  "pi pi-file"),
                            CardLink("/specs", l("Specs"), options: cl => cl.Icon = "pi pi-list-check"),
                        ]);
                        ep.ErrorInfos[403] = ErrorPageInfo(l("Access Denied"), l("You do not have the permision to view the address or data specified."));
                        ep.ErrorInfos[404] = ErrorPageInfo(l("Page Not Found"), l("The page you want to view is etiher deleted or outdated."));
                        ep.ErrorInfos[500] = ErrorPageInfo(l("Unexpected Error"), l("Please contact system administrator."));
                    },
                    data: Computed(Composables.UseError)
                );
            });
        });

        configurator.ConfigureLayoutDescriptors(layouts =>
        {
            configurator.UsingLocalization(l =>
            {
                layouts.Add(DefaultLayout("default", options: dl =>
                {
                    dl.SideMenu = SideMenu(
                        options: sm =>
                        {
                            sm.Menu.AddRange(
                            [
                                SideMenuItem("/", "pi pi-home"),
                                SideMenuItem("/cache", "pi pi-database", options: smi => smi.Title = l("Cache")),
                                SideMenuItem("/data-table", "pi pi-table", options: smi => smi.Title = l("Data Table")),
                                SideMenuItem("/report", "pi pi-file", options: smi => smi.Title = l("Report")),
                                SideMenuItem("/specs", "pi pi-list-check", options: smi => smi.Title = l("Specs"))
                            ]);
                            sm.Footer = LanguageSwitcher();
                        }
                    );
                    dl.Header = Header(options: h =>
                    {
                        h.Sitemap["/"] = HeaderItem("/", options: hi => hi.Icon = "pi pi-home");
                        h.Sitemap["/cache"] = HeaderItem("/cache", options: hi =>
                        {
                            hi.Icon = "pi pi-database";
                            hi.Title = l("Cache");
                        });
                        h.Sitemap["/data-table"] = HeaderItem("/data-table", options: hi =>
                        {
                            hi.Icon = "pi pi-table";
                            hi.Title = l("Data Table");
                        });
                        h.Sitemap["/report"] = HeaderItem("/report", options: hi =>
                        {
                            hi.Icon = "pi pi-file";
                            hi.Title = l("Report");
                        });
                        h.Sitemap["/specs"] = HeaderItem("/specs", options: hi =>
                        {
                            hi.Icon = "pi pi-list-check";
                            hi.Title = l("Specs");
                        });

                        foreach (var spec in _specs)
                        {
                            foreach (var link in spec.Links)
                            {
                                h.Sitemap[$"/specs/{link.Title.Kebaberize()}"] =
                                    HeaderItem($"/specs/{link.Title.Kebaberize()}", options: hi =>
                                    {
                                        hi.Title = l(link.Title);
                                        hi.ParentRoute = "/specs";
                                    });
                            }
                        }
                    });
                }));
            });

            layouts.Add(ModalLayout("modal"));
        });

        configurator.ConfigurePageDescriptors(pages =>
        {
            configurator.UsingLocalization(l =>
            {
                var headers = Inline(new { Authorization = "token-admin-ui" });

                pages.Add(MenuPage("index",
                    links:
                    [
                        CardLink($"/cache", l("Cache"), options: cl =>
                        {
                            cl.Icon = "pi pi-database";
                            cl.Description = l("Showcases the cache behavior");
                        }),
                        CardLink($"/data-table", l("Data Table"), options: cl =>
                        {
                            cl.Icon= "pi pi-table";
                            cl.Description = l("Showcase DataTable component with scrollable and footer options");
                        }),
                        CardLink($"/report", l("Report"), options: cl =>
                        {
                            cl.Icon = "pi pi-file";
                            cl.Description = l("Showcases a report layout with tabs and data panels");
                        }),
                        CardLink($"/specs", l("Specs"), options: cl =>
                        {
                            cl.Icon = "pi pi-list-check";
                            cl.Description = l("All UI Specs are listed here");
                        })
                    ]
                ));

                pages.Add(new ComponentDescriptorAttribute<LoginPage>(new("login") { Layout = "modal" }));
                pages.Add(new ComponentDescriptorAttribute<RoutedPage>(new("page/with/route/pageWithRoute") { Layout = "default" }));

                configurator.UsingDomainModel(domain =>
                {
                    var report = domain.Types[typeof(Report)].GetMembers();
                    var wide = report.Methods[nameof(Report.GetWide)];
                    var left = report.Methods[nameof(Report.GetLeft)];
                    var right = report.Methods[nameof(Report.GetRight)];
                    var first = report.Methods[nameof(Report.GetFirst)];
                    var second = report.Methods[nameof(Report.GetSecond)];

                    pages.Add(ReportPage("report", PageTitle(l("Report"), options: pt => pt.Description = l("Showcases a report layout with tabs and data panels")),
                        options: rp =>
                        {
                            rp.QueryParameters.AddRange(
                            [
                                Parameter("requiredWithDefault",
                                    component: Select(l("Required w/ Default"),
                                        options: s =>
                                        {
                                            s.OptionLabel = "text";
                                            s.OptionValue = "value";
                                        },
                                        data: Inline(new[]
                                        {
                                          new { text = l("Required w/ Default 1"), value = l("rwd-1") },
                                          new { text = l("Required w/ Default 2"), value = l("rwd-2") }
                                        })
                                    ),
                                    options: p =>
                                    {
                                        p.DefaultValue = "rwd-1";
                                        p.Required = true;
                                    }
                                ),
                                Parameter("required",
                                    component: Select(l("Required"), data: Inline(new[] { l("Required 1"), l("Required 2") })),
                                    options: p => p.Required = true
                                ),
                                Parameter("optional",
                                    component: SelectButton(Inline(new[] { l("Optional 1"), l("Optional 2") }), options: sb => sb.AllowEmpty = true)
                                )
                            ]);
                            rp.Tabs.AddRange(
                            [
                                ReportPageTab("single-value",
                                    options: rpt =>
                                    {
                                        rpt.Title = l("Single Value");
                                        rpt.Icon = Icon("pi-box");
                                        rpt.Contents.AddRange(
                                        [
                                            ReportPageTabContent(
                                                component: DataPanel(l(wide.Name),
                                                    content: String(
                                                        data: Remote($"/{wide.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                            headers: headers,
                                                            query: Computed(Composables.UseQuery)
                                                        )
                                                    ),
                                                    options: dp => dp.Collapsed = false
                                                )
                                            ),
                                            ReportPageTabContent(
                                                component: DataPanel(l(left.Name),
                                                    content: String(
                                                        data: Remote($"/{left.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                            headers: headers,
                                                            query: Computed(Composables.UseQuery)
                                                        )
                                                    ),
                                                    options: dp => dp.Collapsed = true
                                                ),
                                                options: rptc => rptc.Narrow = true
                                            ),
                                            ReportPageTabContent(
                                                component: DataPanel(l(right.Name),
                                                    content: String(
                                                        data: Remote($"/{right.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                            headers: headers,
                                                            query: Computed(Composables.UseQuery)
                                                        )
                                                    ),
                                                    options: dp => dp.Collapsed = true
                                                ),
                                                options: rptc => rptc.Narrow = true
                                            )
                                        ]);
                                    }
                                ),
                                ReportPageTab("data-table",
                                    options: rpt =>
                                    {
                                        rpt.Title = l("Data Table");
                                        rpt.Icon = Icon("pi-table");
                                        rpt.Contents.AddRange(
                                        [
                                            ReportPageTabContent(
                                                component: DataPanel(l(first.Name),
                                                    content: DataTable(
                                                        options: dt =>
                                                        {
                                                            dt.Columns.AddRange(
                                                            [
                                                                DataTableColumn("label", options: dtc =>
                                                                {
                                                                    dtc.Title = l("Label");
                                                                    dtc.MinWidth = true;
                                                                    dtc.Exportable = true;
                                                                }),
                                                                DataTableColumn("column1", options: dtc =>
                                                                {
                                                                    dtc.Title = l("Column 1");
                                                                    dtc.Exportable = true;
                                                                }),
                                                                DataTableColumn("column2", options: dtc =>
                                                                {
                                                                    dtc.Title = l("Column 2");
                                                                    dtc.Exportable = true;
                                                                }),
                                                                DataTableColumn("column3", options: dtc =>
                                                                {
                                                                    dtc.Title = l("Column 3");
                                                                    dtc.Exportable = true;
                                                                })
                                                            ]);
                                                            dt.DataKey = "label";
                                                            dt.Paginator = true;
                                                            dt.Rows = 5;
                                                            dt.ExportOptions = DataTableExport(";", l("first"), options: dte =>
                                                            {
                                                                dte.Formatter = "useCsvFormatter";
                                                                dte.ButtonLabel = l("Export as CSV");
                                                                dte.AppendParameters = true;
                                                                dte.ParameterSeparator = "_";
                                                                dte.ParameterFormatter = "useLocaleParameterFormatter";
                                                            });
                                                        },
                                                        data: Remote($"/{first.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                            headers: headers,
                                                            query: Composite([Computed(Composables.UseQuery), Injected()])
                                                        )
                                                    ),
                                                    options: dp => dp.Parameters.Add(
                                                        Parameter("count",
                                                            component: Select(l("Count"), Inline(Enum.GetNames<CountOptions>().Select(name => l(name))), options: s => s.Stateful = true),
                                                            options: p => p.DefaultValue = CountOptions.Default
                                                        )
                                                    )
                                                )
                                            ),
                                            ReportPageTabContent(
                                                component: DataPanel(l(second.Name),
                                                    content: DataTable(
                                                        options: dt =>
                                                        {
                                                            dt.Columns.AddRange(
                                                            [
                                                                DataTableColumn("label", options: dtc =>
                                                                {
                                                                    dtc.Title = l("Label");
                                                                    dtc.MinWidth = true;
                                                                }),
                                                                DataTableColumn("column1", options: dtc => dtc.Title = l("Column 1")),
                                                                DataTableColumn("column2", options: dtc => dtc.Title = l("Column 2")),
                                                                DataTableColumn("column3", options: dtc => dtc.Title = l("Column 3"))
                                                            ]);
                                                            dt.DataKey = "label";
                                                            dt.Paginator = true;
                                                            dt.Rows = 5;
                                                        },
                                                        data: Remote($"/{second.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                            headers: headers,
                                                            query: Composite([Computed(Composables.UseQuery), Injected()])
                                                        )
                                                    ),
                                                    options: dp =>
                                                    {
                                                        dp.Parameters.Add(
                                                            Parameter("count",
                                                                component: SelectButton(Inline(Enum.GetNames<CountOptions>().Select(name => l(name))), options: sb => sb.Stateful = true),
                                                                options: p => p.DefaultValue = CountOptions.Default
                                                            )
                                                        );
                                                        dp.Collapsed = true;
                                                    }
                                                )
                                            )
                                        ]);
                                    }
                                )
                            ]);
                        }
                    ));
                });

                configurator.UsingDomainModel(domain =>
                {
                    pages.Add(ReportPage("data-table", PageTitle(l("Data Table Demo")),
                        options: rp =>
                        {
                            rp.Tabs.AddRange(
                            [
                                ReportPageTab("default",
                                    options: rpt =>
                                    {
                                        rpt.Contents.AddRange(
                                        [
                                            ReportPageTabContent(
                                                DataPanel(l("Data Panel"),
                                                    content: DataTable(
                                                        options: dt =>
                                                        {
                                                            dt.Columns.AddRange(
                                                            [
                                                                .. domain.Types[typeof(TableRow)].GetMembers().Properties.Where(p => p.IsPublic).Select((p, i) =>
                                                                    DataTableColumn(p.Name.Camelize(), options: dtc =>
                                                                    {
                                                                        dtc.Title = l(p.Name.Humanize(LetterCasing.Title));
                                                                        dtc.Exportable = true;
                                                                        dtc.AlignRight = p.PropertyType.Is<string>() ? null : true;
                                                                        dtc.Frozen = i == 0 ? true : null;
                                                                        dtc.MinWidth = i == 0 ? true : null;
                                                                    })
                                                                )
                                                            ]);
                                                            dt.FooterTemplate = DataTableFooter(l("Total"),
                                                                options: dtf =>
                                                                {
                                                                    dtf.Columns.AddRange(
                                                                    [
                                                                        DataTableColumn(nameof(TableWithFooter.FooterColumn1).Camelize(), options: dtc => dtc.AlignRight = true),
                                                                        DataTableColumn(nameof(TableWithFooter.FooterColumn2).Camelize(), options: dtc => dtc.AlignRight = true)
                                                                    ]);
                                                                }
                                                            );
                                                            dt.DataKey = nameof(TableRow.Label).Camelize();
                                                            dt.ItemsProp = "items";
                                                            dt.ScrollHeight = "500px";
                                                            dt.VirtualScrollerOptions = DataTableVirtualScroller(options: dtvs => dtvs.ItemSize = 45);
                                                            dt.ExportOptions = DataTableExport(";", l("data-table-export"), options: dte =>
                                                            {
                                                                dte.Formatter = "useCsvFormatter";
                                                                dte.ButtonLabel = l("Export as CSV");
                                                                dte.AppendParameters = true;
                                                            });
                                                        },
                                                        data: Remote(domain.Types[typeof(DataTable)].GetMembers().Methods[nameof(DataTable.GetTableDataWithFooter)].GetSingle<ActionModelAttribute>().GetRoute(),
                                                            query: Injected(custom: true)
                                                        )
                                                    ),
                                                    options: dp => dp.Parameters.Add(
                                                        Parameter("count",
                                                            component: Select(l("Count"), Inline(new string[]{ "10", "20", "100", "1000", "10000" }, requireLocalization: false)),
                                                            options: p => p.DefaultValue = "10"
                                                        )
                                                    )
                                                )
                                            )
                                        ]);
                                    }
                                )
                            ]);
                        }
                    ));
                });

                configurator.UsingDomainModel(domain =>
                {
                    var report = domain.Types[typeof(CacheSamples)].GetMembers();
                    var getScoped = report.Methods[nameof(CacheSamples.GetScoped)];
                    var getApplication = report.Methods[nameof(CacheSamples.GetApplication)];

                    pages.Add(ReportPage("cache", PageTitle("Cache", options: pt => pt.Description = l("Showcases the cache behavior")),
                        options: rp =>
                        {
                            rp.QueryParameters.Add(
                                Parameter("parameter",
                                    component: Select(l("Parameter"), Inline(new[] { "value_a", "value_b" }, requireLocalization: false)),
                                    options: p =>
                                    {
                                        p.Required = true;
                                        p.DefaultValue = "value_a";
                                    }
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
                                                    content: String(
                                                        data: Remote($"/{getScoped.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                            headers: headers,
                                                            query: Computed(Composables.UseQuery),
                                                            attributes: [("client-cache", "user")]
                                                        )
                                                    )
                                                ),
                                                options: rptc => rptc.Narrow = true
                                            ),
                                            ReportPageTabContent(
                                                component: DataPanel(l(getApplication.Name),
                                                    content: String(
                                                        data: Remote($"/{getApplication.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                            headers: headers,
                                                            query: Computed(Composables.UseQuery),
                                                            attributes: [("client-cache", "application")]
                                                        )
                                                    )
                                                ),
                                                options: rptc => rptc.Narrow = true
                                            )
                                        ]);
                                    }
                                )
                            );
                        }
                    ));
                });

                pages.Add(MenuPage("specs",
                    options: mp =>
                    {
                        mp.FilterPageContextKey = "menu-page";
                        mp.Header = PageTitle(
                            title: l("Specs"),
                            options: pt =>
                            {
                                pt.Description = l("All UI Specs are listed here");
                                pt.Actions.Add(Filter(pageContextKey: "menu-page", options: f => f.Placeholder = l("Filter")));
                            }
                        );
                        mp.Sections.AddRange(
                            _specs.Select(section =>
                                MenuPageSection(
                                    options: mps =>
                                    {
                                        mps.Title = l(section.Name);
                                        mps.Links.AddRange(
                                             section.Links.Select(link =>
                                                Filterable(
                                                    component: CardLink($"/specs/{link.Title.Kebaberize()}", l(link.Title), options: cl =>
                                                    {
                                                        cl.Icon = "pi pi-microchip";
                                                        cl.Description = l(link.Description);
                                                    }),
                                                    options: f => f.Title = l(link.Title)
                                                )
                                            )
                                        );
                                    }
                                )
                            )
                        );
                    }
                ));
            });
        });
    }
}