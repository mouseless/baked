using Baked.Domain.Model;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Admin.Components;

namespace Baked.Theme.Admin;

public static class DomainComponents
{
    public static ComponentDescriptor<ReportPage> TypeReportPage(TypeModelMetadata type, ComponentContext context,
        Action<ReportPage>? options = default
    )
    {
        var (_, l) = context;

        var path = type.Name.Kebaberize();
        var title = PageTitle(l(type.Name), options: pt =>
        {
            pt.Description = l(context.Route.Description);
        });
        var tabs = type.GetSchemas<ReportPage.Tab>(context.Drill("/tabs"));

        return ReportPage(path, title, options: schema =>
        {
            schema.Tabs.AddRange(tabs);

            options.Apply(schema);
        });
    }

    public static ReportPage.Tab TypeReportPageTab(TypeModelMetadata type, ComponentContext context, string id,
        Action<ReportPage.Tab>? options = default
    )
    {
        var (_, l) = context;

        return ReportPageTab(id, options: rpt =>
        {
            rpt.Icon = type.GetComponent(context.Drill($"/{id}/icon"));

            options.Apply(rpt);

            if (rpt.Title is null)
            {
                rpt.Title = l(id.Replace("-", " ").Titleize());
            }
        });
    }
}