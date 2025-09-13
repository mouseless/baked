using Baked.Domain.Model;
using Baked.RestApi.Model;
using Baked.Theme;
using Baked.Theme.Admin;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Admin.DomainDatas;
using static Baked.Theme.Admin.Components;

namespace Baked.Test.Theme.Custom;

public static class DomainComponents
{
    #region DataTable

    public static ComponentDescriptor<Baked.Theme.Admin.DataTable> ReportRowListActionDataTable(MethodModel method, ComponentContext context,
        Action<RemoteData>? dataOptions = default,
        bool exportable = true
    )
    {
        var (domain, l) = context;

        return DataTable(
            options: dt =>
            {
                dt.Columns.AddRange(
                [
                    DataTableColumn("label", options: dtc =>
                    {
                        dtc.Title = l("Label");
                        dtc.MinWidth = true;
                        dtc.Exportable = exportable ? true : null;
                    }),
                    DataTableColumn("column1", options: dtc =>
                    {
                        dtc.Title = l("Column 1");
                        dtc.Exportable = exportable ? true : null;
                    }),
                    DataTableColumn("column2", options: dtc =>
                    {
                        dtc.Title = l("Column 2");
                        dtc.Exportable = exportable ? true : null;
                    }),
                    DataTableColumn("column3", options: dtc =>
                    {
                        dtc.Title = l("Column 3");
                        dtc.Exportable = exportable ? true : null;
                    })
                ]);
                dt.DataKey = "label";
                dt.Paginator = true;
                dt.Rows = 5;
                dt.ExportOptions = exportable
                    ? DataTableExport(";", l($"{method.Name}.ExportFileName"), options: dte =>
                    {
                        dte.Formatter = "useCsvFormatter";
                        dte.ButtonLabel = l("Export as CSV");
                        dte.AppendParameters = true;
                        dte.ParameterSeparator = "_";
                        dte.ParameterFormatter = "useLocaleParameterFormatter";
                    })
                    : null;
            },
            data: MethodRemote(method, options: dataOptions)
        );
    }

    public static ComponentDescriptor<Baked.Theme.Admin.DataTable> TableWithFooterActionDataTable(MethodModel method, ComponentContext context)
    {
        var (domain, l) = context;

        return DataTable(
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
            data: MethodRemote(method)
        );
    }

    #endregion

    #region Parameter

    public static Parameter EnumSelectButtonParameter(ParameterModel parameter, ComponentContext context,
        bool requireLocalization = true,
        Action<Parameter>? options = default,
        Action<SelectButton>? selectButtonOptions = default
    )
    {
        var (domain, l) = context;
        var api = parameter.Get<ParameterModelAttribute>();

        return ParameterParameter(parameter,
            component: p => EnumSelectButton(p.ParameterType, context.Drill("Component"),
                options: s =>
                {
                    s.AllowEmpty = api.IsOptional ? true : null;
                    s.Stateful = context.Path.Contains("Tabs") ? true : null;

                    selectButtonOptions.Apply(s);
                },
                requireLocalization: requireLocalization
            ),
            options: p =>
            {
                if (!api.IsOptional)
                {
                    p.DefaultValue = parameter.ParameterType.SkipNullable().GetEnumNames().First();
                }

                options.Apply(p);
            }
        );
    }

    public static Parameter EnumSelectParameter(ParameterModel parameter, ComponentContext context,
        bool requireLocalization = true,
        Action<Parameter>? options = default,
        Action<Select>? selectOptions = default
    )
    {
        var (domain, l) = context;
        var api = parameter.Get<ParameterModelAttribute>();

        return ParameterParameter(parameter,
            component: p => EnumSelect(l(p.Name.Titleize()), p.ParameterType, context.Drill("Component"),
                options: s =>
                {
                    s.ShowClear = api.IsOptional ? true : null;
                    s.Stateful = context.Path.Contains("Tabs") ? true : null;

                    selectOptions.Apply(s);
                },
                requireLocalization: requireLocalization
            ),
            options: p =>
            {
                if (!api.IsOptional)
                {
                    p.DefaultValue = parameter.ParameterType.SkipNullable().GetEnumNames().First();
                }

                options.Apply(p);
            }
        );
    }

    public static Parameter ParameterParameter(ParameterModel parameter, Func<ParameterModel, IComponentDescriptor> component,
        Action<Parameter>? options = default
    )
    {
        var api = parameter.Get<ParameterModelAttribute>();

        return Parameter(api.Name, component(parameter), options: p =>
        {
            p.Required = !api.IsOptional ? true : null;
            if (api.DefaultValue is not null)
            {
                p.DefaultValue = api.DefaultValue;
            }

            options.Apply(p);
        });
    }

    #endregion

    #region Select

    public static ComponentDescriptor<Select> EnumSelect(string label, TypeModel enumType, ComponentContext context,
        Action<Select>? options = default,
        bool requireLocalization = true
    ) => Select(label, EnumInline(enumType, context.Drill("Data"), requireLocalization: requireLocalization),
        options: s =>
        {
            if (requireLocalization)
            {
                s.OptionLabel = "text";
                s.OptionValue = "value";
            }

            options.Apply(s);
        }
    );

    #endregion

    #region SelectButton

    public static ComponentDescriptor<SelectButton> EnumSelectButton(TypeModel enumType, ComponentContext context,
        Action<SelectButton>? options = default,
        bool requireLocalization = true
    ) => SelectButton(EnumInline(enumType, context.Drill("Data"), requireLocalization: requireLocalization),
        options: sb =>
        {
            if (requireLocalization)
            {
                sb.OptionLabel = "text";
                sb.OptionValue = "value";
            }

            options.Apply(sb);
        }
    );

    #endregion

    #region String

    public static ComponentDescriptor<Baked.Theme.Admin.String> MethodString(MethodModel method, ComponentContext context,
        Action<Baked.Theme.Admin.String>? options = default
    ) => String(
        data: method.GetSchema<RemoteData>(context.Drill(nameof(IComponentDescriptor.Data))),
        options: options
    );

    #endregion
}