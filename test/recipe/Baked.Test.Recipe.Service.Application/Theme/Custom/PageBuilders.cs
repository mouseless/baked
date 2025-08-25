using Baked.Business;
using Baked.RestApi.Model;
using Baked.Test.Caching;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Admin.Components;
using static Baked.Test.Theme.Custom.DomainComponents;
using static Baked.Test.Theme.Custom.DomainDatas;
using static Baked.Ui.Datas;

namespace Baked.Test.Theme.Custom;

public static class PageBuilders
{
    public static IComponentDescriptor Menu(PageContext context)
    {
        var l = context.NewLocaleKey;

        if (context.Page.Index)
        {
            return MenuPage(context.Page.Name,
                links: context.Sitemap
                    .Where(smp => !smp.Index && smp.SideMenu)
                    .Select(smp => smp.AsCardLink(l))
            );
        }

        var sections = context.Sitemap.GroupBy(smp => smp.Section);
        if (sections.Count() <= 1)
        {
            return MenuPage(context.Page.Name,
                links: context.Sitemap
                    .Where(p => p.ParentRoute == context.Page.Route)
                    .Select(p => p.AsCardLink(l)),
                options: mp =>
                {
                    mp.Header = PageTitle(
                        title: l(context.Page.Title),
                        options: pt => pt.Description = l(context.Page.Description)
                    );
                }
            );
        }

        return MenuPage(context.Page.Name,
            options: mp =>
            {
                mp.Header = PageTitle(context.Page.Title, options: pt =>
                {
                    pt.Description = l(context.Page.Description);
                    pt.Actions.Add(Filter("menu-page", options: f => f.Placeholder = l("Filter")));
                });
                mp.FilterPageContextKey = "menu-page";
                mp.Sections.AddRange(
                    sections.Select(g => MenuPageSection(
                        options: mps =>
                        {
                            mps.Title = l(g.Key);
                            mps.Links.AddRange(g
                                .Where(p => p.ParentRoute == context.Page.Route)
                                .Select(p => Filterable(p.AsCardLink(l), options: f => f.Title = l(p.Title)))
                            );
                        }
                    )).Where(s => s.Links.Any())
                );
            }
        );
    }

    public static IComponentDescriptor Cache(PageContext context)
    {
        var (domain, l) = context;
        var headers = Inline(new { Authorization = "token-admin-ui" });

        var report = domain.Types[typeof(CacheSamples)].GetMembers();
        var with = report.Methods.Having<InitializerAttribute>().First().DefaultOverload;
        var getScoped = report.Methods[nameof(CacheSamples.GetScoped)];
        var getApplication = report.Methods[nameof(CacheSamples.GetApplication)];

        return ReportPage("cache", PageTitle("Cache", options: pt => pt.Description = l("Showcases the cache behavior")),
            options: rp =>
            {
                rp.QueryParameters.Add(
                    EnumSelectParameter(with.Parameters["parameter"], context.CreateComponentContext("/parameters/parameter"))
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
    }

    public static IComponentDescriptor DataTable(PageContext context)
    {
        var (domain, l) = context;

        return ReportPage("data-table", PageTitle(l("Data Table Demo")),
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
                                        content: Baked.Theme.Admin.Components.DataTable(
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
                                            data: Remote(domain.Types[typeof(DataTable)].GetMembers().Methods[nameof(Theme.DataTable.GetTableDataWithFooter)].GetSingle<ActionModelAttribute>().GetRoute(),
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
        );
    }

    public static IComponentDescriptor Report(PageContext context)
    {
        var (domain, l) = context;
        var headers = Inline(new { Authorization = "token-admin-ui" });

        var report = domain.Types[typeof(Report)].GetMembers();
        var wide = report.Methods[nameof(Theme.Report.GetWide)];
        var left = report.Methods[nameof(Theme.Report.GetLeft)];
        var right = report.Methods[nameof(Theme.Report.GetRight)];
        var first = report.Methods[nameof(Theme.Report.GetFirst)];
        var second = report.Methods[nameof(Theme.Report.GetSecond)];

        return ReportPage("report", PageTitle(l("Report"), options: pt => pt.Description = l("Showcases a report layout with tabs and data panels")),
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
                                            data: ActionRemote(wide, headers: headers)
                                        ),
                                        options: dp => dp.Collapsed = false
                                    )
                                ),
                                ReportPageTabContent(
                                    component: DataPanel(l(left.Name),
                                        content: String(
                                            data: ActionRemote(left, headers: headers)
                                        ),
                                        options: dp => dp.Collapsed = true
                                    ),
                                    options: rptc => rptc.Narrow = true
                                ),
                                ReportPageTabContent(
                                    component: DataPanel(l(right.Name),
                                        content: String(
                                            data: ActionRemote(left, headers: headers)
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
                                        content: Baked.Theme.Admin.Components.DataTable(
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
                                            data: ActionRemote(first, headers: headers)
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
                                        content: Baked.Theme.Admin.Components.DataTable(
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
                                            data: ActionRemote(second, headers: headers)
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
        );
    }
}