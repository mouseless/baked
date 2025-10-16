using Baked.Domain.Model;
using Baked.RestApi.Model;
using Baked.Ui;
using Baked.Ui.Component;
using Humanizer;

namespace Baked.Theme.Default;

public static class DomainComponents
{
    public static ComponentDescriptor<ReportPage> TypeReportPage(
#pragma warning disable IDE0060
        TypeModelMetadata type,
#pragma warning restore IDE0060
        ComponentContext context,
        Action<ReportPage>? options = default
    )
    {
        context = context.Drill(nameof(ReportPage));
        var (_, l) = context;

        var path = context.Route.Path.Trim('/');
        var title = Components.PageTitle(l(context.Route.Title), options: pt => pt.Description = l(context.Route.Description));

        return Components.ReportPage(path, title, options: options);
    }

    public static ReportPage.Tab TypeReportPageTab(TypeModelMetadata type, ComponentContext context, string name,
        Action<ReportPage.Tab>? options = default
    )
    {
        context = context.Drill(name);
        var (_, l) = context;

        return Components.ReportPageTab(name.Kebaberize(), options: rpt =>
        {
            rpt.Icon = type.GetComponent(context.Drill(nameof(ReportPage.Tab.Icon)));

            options.Apply(rpt);
        });
    }

    public static ReportPage.Tab.Content MethodReportPageTabContent(MethodModel method, ComponentContext context,
        Action<ReportPage.Tab.Content>? options = default
    )
    {
        context = context.Drill(method.Name);

        return Components.ReportPageTabContent(method.GetRequiredComponent(context.Drill(nameof(ReportPage.Tab.Content.Component))), method.Name.Kebaberize(),
            options: options
        );
    }

    public static ComponentDescriptor<DataPanel> MethodDataPanel(MethodModel method, ComponentContext context,
        Action<DataPanel>? options = default
    )
    {
        context = context.Drill(nameof(DataPanel));
        var (_, l) = context;

        return Components.DataPanel(
            method.GetRequiredSchema<InlineData>(context.Drill(nameof(DataPanel.Title))),
            method.GetRequiredComponent(context.Drill(nameof(DataPanel.Content))),
            options: options
        );
    }

    public static Parameter ParameterParameter(ParameterModel parameter, ComponentContext context,
        Action<Parameter>? options = default
    )
    {
        context = context.Drill(parameter.Name);
        var api = parameter.Get<ParameterModelAttribute>();

        return Components.Parameter(api.Name, parameter.GetRequiredComponent(context.Drill(nameof(Parameter.Component))), options: options);
    }

    public static ComponentDescriptor<Select> EnumSelect(ParameterModel parameter, ComponentContext context,
        Action<Select>? options = default
    )
    {
        context = context.Drill(nameof(Select));
        var (_, l) = context;
        if (!parameter.ParameterType.TryGetMetadata(out var metadata)) { throw new($"{parameter.ParameterType.CSharpFriendlyFullName} cannot be used, its metadata is not present in domain model"); }

        var data = metadata.GetRequiredSchema<InlineData>(context.Drill(nameof(IComponentDescriptor.Data)));

        return Components.Select(l(parameter.Name.Titleize()), data, options: options);
    }

    public static ComponentDescriptor<SelectButton> EnumSelectButton(ParameterModel parameter, ComponentContext context,
        Action<SelectButton>? options = default
    )
    {
        context = context.Drill(nameof(SelectButton));
        var (_, l) = context;
        if (!parameter.ParameterType.TryGetMetadata(out var metadata)) { throw new($"{parameter.ParameterType.CSharpFriendlyFullName} cannot be used, its metadata is not present in domain model"); }

        var data = metadata.GetRequiredSchema<InlineData>(context.Drill(nameof(IComponentDescriptor.Data)));

        return Components.SelectButton(data, options: options);
    }

    public static ComponentDescriptor<DataTable> MethodDataTable(MethodModel method, ComponentContext context,
        Action<DataTable>? options = default
    )
    {
        context = context.Drill(nameof(DataTable));
        var (_, l) = context;

        return Components.DataTable(
            options: dt =>
            {
                dt.ExportOptions = method.GetSchema<DataTable.Export>(context.Drill(nameof(DataTable.ExportOptions)));
                dt.FooterTemplate = method.GetSchema<DataTable.Footer>(context.Drill(nameof(DataTable.FooterTemplate)));
                dt.VirtualScrollerOptions = method.GetSchema<DataTable.VirtualScroller>(context.Drill(nameof(DataTable.VirtualScrollerOptions)));

                options.Apply(dt);
            },
            data: method.GetRequiredSchema<RemoteData>(context.Drill(nameof(IComponentDescriptor.Data)))
        );
    }

    public static DataTable.Export MethodDataTableExport(MethodModel method, ComponentContext context,
        Action<DataTable.Export>? options = default
    )
    {
        var (_, l) = context;

        return Components.DataTableExport(";", l($"{method.Name}.ExportFileName"), options: options);
    }

    public static DataTable.Footer MethodDataTableFooter(MethodModel method, ComponentContext context,
        Action<DataTable.Footer>? options = default
    )
    {
        var (_, l) = context;

        return Components.DataTableFooter(l($"{method.Name}.FooterLabel"), options: options);
    }

    public static DataTable.Column PropertyDataTableColumn(PropertyModel property, ComponentContext context,
        Action<DataTable.Column>? options = default
    )
    {
        context = context.Drill(property.Name);
        var (_, l) = context;

        if (!property.TryGet<DataAttribute>(out var data))
        {
            data = new(property.Name.Camelize());
        }

        return Components.DataTableColumn(data.Prop,
            options: dtc =>
            {
                dtc.Component = property.GetRequiredSchema<Conditional>(context.Drill(nameof(DataTable.Column.Component)));

                options.Apply(dtc);
            }
        );
    }

    public static Conditional PropertyConditional(PropertyModel property, ComponentContext context,
        Action<Conditional>? options = default
    ) => Components.Conditional(
        options: c =>
        {
            c.Fallback = property.GetRequiredComponent(context.Drill(nameof(Conditional.Fallback)));

            options.Apply(c);
        }
    );
}