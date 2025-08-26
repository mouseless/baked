using Baked.Business;
using Baked.Test.Caching;
using Baked.Theme;
using Baked.Ui;

using static Baked.Theme.Admin.Components;
using static Baked.Test.Theme.Custom.DomainComponents;
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

        var cacheSamples = domain.Types[typeof(CacheSamples)].GetMembers();
        var initializer = cacheSamples.Methods.Having<InitializerAttribute>().First().DefaultOverload;
        var getScoped = cacheSamples.Methods[nameof(CacheSamples.GetScoped)];
        var getApplication = cacheSamples.Methods[nameof(CacheSamples.GetApplication)];

        return ReportPage("cache", PageTitle("Cache", options: pt => pt.Description = l("Showcases the cache behavior")),
            options: rp =>
            {
                rp.QueryParameters.Add(
                    EnumSelectParameter(initializer.Parameters["parameter"], context.CreateComponentContext("/parameters/parameter"),
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
    }

    public static IComponentDescriptor DataTable(PageContext context)
    {
        var (domain, l) = context;
        var dataTable = domain.Types[typeof(DataTable)].GetMembers();
        var getTableDataWithFooter = dataTable.Methods[nameof(Theme.DataTable.GetTableDataWithFooter)];

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
                                content: TableWithFooterActionDataTable(getTableDataWithFooter, context.CreateComponentContext("/tabs/default/contents/0")),
                                options: dp => dp.Parameters.Add(
                                    EnumSelectParameter(getTableDataWithFooter.DefaultOverload.Parameters["count"], context.CreateComponentContext("/parameters/count"),
                                        requireLocalization: false
                                    )
                                )
                            )
                        )
                    ]);
                })
            ]);
        });
    }

    public static IComponentDescriptor Report(PageContext context)
    {
        var (domain, l) = context;
        var headers = Inline(new { Authorization = "token-admin-ui" });

        var report = domain.Types[typeof(Report)].GetMembers();
        var initializer = report.Methods.Having<InitializerAttribute>().First().DefaultOverload;
        var wide = report.Methods[nameof(Theme.Report.GetWide)];
        var left = report.Methods[nameof(Theme.Report.GetLeft)];
        var right = report.Methods[nameof(Theme.Report.GetRight)];
        var first = report.Methods[nameof(Theme.Report.GetFirst)];
        var second = report.Methods[nameof(Theme.Report.GetSecond)];

        return ReportPage("report", PageTitle(l("Report"), options: pt => pt.Description = l("Showcases a report layout with tabs and data panels")), options: rp =>
        {
            rp.QueryParameters.AddRange(
            [
                EnumSelectParameter(initializer.Parameters["requiredWithDefault"], context.CreateComponentContext("/parameters/requiredWithDefault")),
                EnumSelectParameter(initializer.Parameters["required"], context.CreateComponentContext("/parameters/required"), options: p => p.Default = null),
                EnumSelectButtonParameter(initializer.Parameters["optional"], context.CreateComponentContext("/parameters/optional"))
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
                                content: ReportRowListActionDataTable(first, context.CreateComponentContext("/tabs/data-table/contents/0/content"),
                                    dataOptions: rd => rd.Headers = headers
                                ),
                                options: dp => dp.Parameters.Add(
                                    EnumSelectParameter(first.DefaultOverload.Parameters["count"], context.CreateComponentContext("/tabs/data-table/contents/0/parameters/count"),
                                        selectOptions: s => s.ShowClear = null
                                    )
                                )
                            )
                        ),
                        ReportPageTabContent(
                            component: DataPanel(l(second.Name),
                                content: ReportRowListActionDataTable(second, context.CreateComponentContext("/tabs/data-table/contents/1/content"),
                                    exportable: false,
                                    dataOptions: rd => rd.Headers = headers
                                ),
                                options: dp =>
                                {
                                    dp.Parameters.Add(
                                        EnumSelectButtonParameter(second.DefaultOverload.Parameters["count"], context.CreateComponentContext("/tabs/data-table/contents/1/parameters/count"),
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
    }
}