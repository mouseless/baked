using Baked.Domain.Model;
using Baked.RestApi.Model;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Admin.Components;
using static Baked.Theme.Admin.DomainDatas;
using static Baked.Ui.Datas;

namespace Baked.Theme.Admin;

public static class DomainComponents
{
    public static ComponentDescriptor<ReportPage> TypeReportPage(TypeModelMetadata type, ComponentContext context,
        Action<ReportPage>? options = default
    )
    {
        context = context.Drill(nameof(ReportPage));
        var (_, l) = context;
        var path = type.Name.Kebaberize();
        var title = PageTitle(l(type.Name), options: pt => pt.Description = l(context.Route.Description));
        var tabs = type.GetSchemas<ReportPage.Tab>(context.Drill(nameof(ReportPage.Tabs)));

        return ReportPage(path, title, options: rp =>
        {
            rp.Tabs.AddRange(tabs);

            options.Apply(rp);
        });
    }

    public static ReportPage.Tab TypeReportPageTab(TypeModelMetadata type, ComponentContext context, string id,
        Action<ReportPage.Tab>? options = default
    )
    {
        context = context.Drill(id);
        var (_, l) = context;

        return ReportPageTab(id.Kebaberize(), options: rpt =>
        {
            rpt.Icon = type.GetComponent(context.Drill(nameof(ReportPage.Tab.Icon)));

            options.Apply(rpt);

            if (rpt.Title is null)
            {
                rpt.Title = l(id.Titleize()); // moved after apply to avoid an unecessary `l` call
            }
        });
    }

    public static ReportPage.Tab.Content MethodReportPageTabContent(MethodModel method, ComponentContext context,
        Action<ReportPage.Tab.Content>? options = default
    ) => ReportPageTabContent(method.GetRequiredComponent(context.Drill(nameof(ReportPage.Tab.Content.Component))),
        options: options
    );

    public static ComponentDescriptor<DataPanel> MethodDataPanel(MethodModel method, ComponentContext context,
        Action<DataPanel>? options = default
    )
    {
        var (_, l) = context;

        return DataPanel(
            Inline(l($"{method.DefaultOverload.DeclaringType?.Name}.{method.Name}.Title")),
            method.GetRequiredComponent(context.Drill(nameof(DataPanel), method.Name, nameof(DataPanel.Content))),
            options: options
        );
    }

    public static Parameter ParameterParameter(ParameterModel parameter, ComponentContext context,
        Action<Parameter>? options = default
    )
    {
        context = context.Drill(parameter.Name);
        var api = parameter.Get<ParameterModelAttribute>();

        return Parameter(api.Name, parameter.GetRequiredComponent(context.Drill(nameof(Parameter.Component))),
            options: p =>
            {
                p.Required = !api.IsOptional ? true : null;
                if (api.DefaultValue is not null)
                {
                    p.DefaultValue = api.DefaultValue;
                }

                options.Apply(p);
            }
        );
    }

    public static ComponentDescriptor<Select> EnumSelect(ParameterModel parameter, ComponentContext context,
        Action<Select>? options = default
    )
    {
        var (_, l) = context;
        var api = parameter.Get<ParameterModelAttribute>();

        return Select(l(parameter.Name.Titleize()), EnumInline(parameter.ParameterType, context.Drill(nameof(IComponentDescriptor.Data))),
            options: s =>
            {
                s.ShowClear = api.IsOptional ? true : null;
                s.OptionLabel = "text";
                s.OptionValue = "value";

                options.Apply(s);
            }
        );
    }

    public static ComponentDescriptor<SelectButton> EnumSelectButton(ParameterModel parameter, ComponentContext context,
        Action<SelectButton>? options = default
    )
    {
        var (_, l) = context;
        var api = parameter.Get<ParameterModelAttribute>();

        return SelectButton(EnumInline(parameter.ParameterType, context.Drill(nameof(IComponentDescriptor.Data))),
            options: sb =>
            {
                sb.AllowEmpty = api.IsOptional ? true : null;
                sb.OptionLabel = "text";
                sb.OptionValue = "value";

                options.Apply(sb);
            }
        );
    }

    public static ComponentDescriptor<DataTable> MethodDataTable(MethodModel method, ComponentContext context,
        Action<DataTable>? options = default
    )
    {
        var (_, l) = context;

        return DataTable(
            options: dt =>
            {
                if (method.DefaultOverload.ReturnType.TryGetMetadata(out var returnType))
                {
                    dt.Columns.AddRange(returnType.GetSchemas<DataTable.Column>(context.Drill(nameof(DataTable.Columns))));
                }

                dt.ExportOptions = method.GetSchema<DataTable.Export>(context.Drill(nameof(DataTable.Export)));
                dt.FooterTemplate = method.GetSchema<DataTable.Footer>(context.Drill(nameof(DataTable.Footer)));
                dt.VirtualScrollerOptions = method.GetSchema<DataTable.VirtualScroller>(context.Drill(nameof(DataTable.VirtualScroller)));

                options.Apply(dt);
            },
            data: method.GetRequiredSchema<RemoteData>(context.Drill(nameof(IComponentDescriptor.Data)))
        );
    }
}