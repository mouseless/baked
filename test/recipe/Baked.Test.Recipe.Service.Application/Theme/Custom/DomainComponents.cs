using Baked.Domain.Model;
using Baked.RestApi.Model;
using Baked.Theme.Admin;
using Baked.Ui;
using Humanizer;

using static Baked.Test.Theme.Custom.DomainDatas;
using static Baked.Theme.Admin.Components;
using static Baked.Ui.UiLayer;

namespace Baked.Test.Theme.Custom;

public static class DomainComponents
{
    #region DataTable

    public static ComponentDescriptorAttribute<Baked.Theme.Admin.DataTable> TableWithFooterActionDataTable(MethodModel method, ComponentContext context)
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
            data: ActionRemote(method)
        );
    }

    #endregion

    #region Parameter

    public static Parameter EnumSelectButtonParameter(ParameterModel parameter, ComponentContext context,
        bool requireLocalization = true,
        Action<Parameter>? options = default
    )
    {
        var (domain, l) = context;
        var api = parameter.GetSingle<ParameterModelAttribute>();

        return ParameterParameter(parameter,
            component: p => EnumSelectButton(p.ParameterType,
                options: s => s.AllowEmpty = api.IsOptional ? true : null,
                l: requireLocalization ? l : null
            ),
            options: p =>
            {
                if (!api.IsOptional)
                {
                    p.DefaultValue = parameter.ParameterType.GetEnumNames().First();
                }

                options.Apply(p);
            }
        );
    }

    public static Parameter EnumSelectParameter(ParameterModel parameter, ComponentContext context,
        bool requireLocalization = true,
        Action<Parameter>? options = default
    )
    {
        var (domain, l) = context;
        var api = parameter.GetSingle<ParameterModelAttribute>();

        return ParameterParameter(parameter,
            component: p => EnumSelect(l(p.Name.Titleize()), p.ParameterType,
                options: s => s.ShowClear = api.IsOptional ? true : null,
                l: requireLocalization ? l : null
            ),
            options: p =>
            {
                if (!api.IsOptional)
                {
                    p.DefaultValue = parameter.ParameterType.GetEnumNames().First();
                }

                options.Apply(p);
            }
        );
    }

    public static Parameter ParameterParameter(ParameterModel parameter, Func<ParameterModel, IComponentDescriptor> component,
        Action<Parameter>? options = default
    )
    {
        var api = parameter.GetSingle<ParameterModelAttribute>();

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

    public static ComponentDescriptorAttribute<Select> EnumSelect(string label, TypeModel enumType,
        Action<Select>? options = default,
        NewLocaleKey? l = default
    ) => Select(label, EnumInline(enumType, l: l),
        options: s =>
        {
            if (l is not null)
            {
                s.OptionLabel = "text";
                s.OptionValue = "value";
            }

            options.Apply(s);
        }
    );

    #endregion

    #region SelectButton

    public static ComponentDescriptorAttribute<SelectButton> EnumSelectButton(TypeModel enumType,
        Action<SelectButton>? options = default,
        NewLocaleKey? l = default
    ) => SelectButton(EnumInline(enumType, l: l),
        options: sb =>
        {
            if (l is not null)
            {
                sb.OptionLabel = "text";
                sb.OptionValue = "value";
            }

            options.Apply(sb);
        }
    );

    #endregion

    #region String

    public static ComponentDescriptorAttribute<Baked.Theme.Admin.String> ActionString(MethodModel method) =>
        String(data: ActionRemote(method));

    #endregion
}