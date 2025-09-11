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
        var tabs = type.GetSchemas<ReportPage.Tab>(context.CreateComponentContext("/tabs"));

        return ReportPage(path, title, options: schema =>
        {
            schema.Tabs.AddRange(tabs);

            options.Apply(schema);
        });
    }

    public static ReportPage.Tab TypeReportPageTab(TypeModelMetadata type, ComponentContext context, string title,
        Action<ReportPage.Tab>? options = default
    )
    {
        var (_, l) = context;

        var id = title.Kebaberize();
        context = context.CreateComponentContext($"/{id}");
        title = l(title);

        return ReportPageTab(id, options: rpt =>
        {
            rpt.Title = title;

            options.Apply(rpt);
        });
    }
}