using Baked.Domain.Model;
using Baked.RestApi.Model;
using Baked.Ui;
using Humanizer;

using B = Baked.Ui.Components;

namespace Baked.Theme.Default;

public static class DomainComponents
{
    public static ComponentDescriptor<MissingComponent> CustomAttributesMissingComponent(ICustomAttributesModel metadata, ComponentContext context,
        Action<MissingComponent>? options = default
    ) => B.MissingComponent(options: mc =>
    {
        mc.Path.AddRange(context.Path.GetParts());
        mc.Source = B.MissingComponentDomainSource(metadata.GetType().Name, options: mcds =>
        {
            mcds.Path.AddRange(metadata.CustomAttributes.Name.Split('.'));
        });

        options.Apply(mc);
    });

    public static ComponentDescriptor<SimplePage> TypeSimplePage(TypeModelMetadata type, ComponentContext context,
        Action<SimplePage>? options = default
    )
    {
        context = context.Drill(nameof(SimplePage));
        var (_, l) = context;

        var path = context.Route.Path.Trim('/');
        var title =
            type.GetComponent(context.Drill(nameof(SimplePage.Title))) ??
            TypePageTitle(type, context.Drill(nameof(SimplePage.Title)));

        return B.SimplePage(path, title, options: options);
    }

    public static ComponentDescriptor<PageTitle> TypePageTitle(
#pragma warning disable IDE0060
        TypeModelMetadata type,
#pragma warning restore IDE0060
        ComponentContext context,
        Action<PageTitle>? options = default
    )
    {
        context = context.Drill(nameof(PageTitle));
        var (_, l) = context;

        return B.PageTitle(l(context.Route.Title), options: pt =>
        {
            pt.Description = l(context.Route.Description);

            options.Apply(pt);
        });
    }

    public static ComponentDescriptor<FormPage> MethodFormPage(MethodModel method, ComponentContext context,
        Action<FormPage>? options = default
    )
    {
        context = context.Drill(nameof(FormPage));
        var (_, l) = context;

        var path = context.Route.Path.Trim('/');
        var title =
            method.GetComponent<PageTitle>(context.Drill(nameof(FormPage.Title))) as ComponentDescriptor<PageTitle> ??
            MethodPageTitle(method, context.Drill(nameof(FormPage.Title)));
        var button =
            method.GetComponent<Button>(context.Drill(nameof(FormPage.Button))) as ComponentDescriptor<Button> ??
            B.Button(l("Save"));

        return B.FormPage(path, title, button,
            action: method.GetSchema<RemoteAction>(context.Drill(nameof(IComponentDescriptor.Action))),
            options: options
        );
    }

    public static ComponentDescriptor<PageTitle> MethodPageTitle(
#pragma warning disable IDE0060
        MethodModel method,
#pragma warning restore IDE0060
        ComponentContext context,
        Action<PageTitle>? options = default
    )
    {
        context = context.Drill(nameof(PageTitle));
        var (_, l) = context;

        return B.PageTitle(l(context.Route.Title), options: pt =>
        {
            pt.Description = l(context.Route.Description);

            options.Apply(pt);
        });
    }

    public static ComponentDescriptor<TabbedPage> TypeTabbedPage(TypeModelMetadata type, ComponentContext context,
        Action<TabbedPage>? options = default
    )
    {
        context = context.Drill(nameof(TabbedPage));
        var (_, l) = context;

        var path = context.Route.Path.Trim('/');
        var title =
            type.GetComponent<PageTitle>(context.Drill(nameof(TabbedPage.Title))) as ComponentDescriptor<PageTitle> ??
            TypePageTitle(type, context.Drill(nameof(TabbedPage.Title)));

        return B.TabbedPage(path, title, options: options);
    }

    public static Tab TypeTab(TypeModelMetadata type, ComponentContext context, string name,
        Action<Tab>? options = default
    )
    {
        context = context.Drill(name);
        var (_, l) = context;

        return B.Tab(name.Kebaberize(), options: t =>
        {
            t.Icon = type.GetComponent(context.Drill(nameof(Tab.Icon)));

            options.Apply(t);
        });
    }

    public static Content MethodContent(MethodModel method, ComponentContext context,
        Action<Content>? options = default
    )
    {
        context = context.Drill(method.Name);

        return B.Content(method.GetRequiredComponent(context.Drill(nameof(Content.Component))), method.Name.Kebaberize(),
            options: options
        );
    }

    public static ComponentDescriptor<DataPanel> MethodDataPanel(MethodModel method, ComponentContext context,
        Action<DataPanel>? options = default
    )
    {
        context = context.Drill(nameof(DataPanel));
        var (_, l) = context;

        return B.DataPanel(
            method.GetRequiredSchema<InlineData>(context.Drill(nameof(DataPanel.Title))),
            method.GetRequiredComponent(context.Drill(nameof(DataPanel.Content))),
            options: options
        );
    }

    public static Input ParameterInput(ParameterModel parameter, ComponentContext context,
        Action<Input>? options = default
    )
    {
        context = context.Drill(parameter.Name);
        var api = parameter.Get<ParameterModelAttribute>();

        return B.Input(api.Name, parameter.GetRequiredComponent(context.Drill(nameof(Input.Component))), options: options);
    }

    public static ComponentDescriptor<InputText> ParameterInputText(ParameterModel parameter, ComponentContext context,
        Action<InputText>? options = default
    )
    {
        context = context.Drill(nameof(InputText));
        var (_, l) = context;

        return B.InputText(l(parameter.Name.Titleize()), options: options);
    }

    public static ComponentDescriptor<InputNumber> ParameterInputNumber(ParameterModel parameter, ComponentContext context,
        Action<InputNumber>? options = default
    )
    {
        context = context.Drill(nameof(InputNumber));
        var (_, l) = context;

        return B.InputNumber(l(parameter.Name.Titleize()), options: options);
    }

    public static ComponentDescriptor<Select> ParameterSelect(ParameterModel parameter, ComponentContext context,
        Action<Select>? options = default
    )
    {
        context = context.Drill(nameof(Select));
        var (_, l) = context;

        if (!parameter.ParameterType.TryGetMetadata(out var metadata)) { throw new($"{parameter.ParameterType.CSharpFriendlyFullName} cannot be used, its metadata is not present in domain model"); }

        var data = metadata.GetRequiredSchema<InlineData>(context.Drill(nameof(IComponentDescriptor.Data)));

        return B.Select(l(parameter.Name.Titleize()), data, options: options);
    }

    public static ComponentDescriptor<SelectButton> ParameterSelectButton(ParameterModel parameter, ComponentContext context,
        Action<SelectButton>? options = default
    )
    {
        context = context.Drill(nameof(SelectButton));
        var (_, l) = context;

        if (!parameter.ParameterType.TryGetMetadata(out var metadata)) { throw new($"{parameter.ParameterType.CSharpFriendlyFullName} cannot be used, its metadata is not present in domain model"); }

        var data = metadata.GetRequiredSchema<InlineData>(context.Drill(nameof(IComponentDescriptor.Data)));

        return B.SelectButton(data, options: options);
    }

    public static ComponentDescriptor<DataTable> MethodDataTable(MethodModel method, ComponentContext context,
        Action<DataTable>? options = default
    )
    {
        context = context.Drill(nameof(DataTable));
        var (_, l) = context;

        return B.DataTable(
            options: dt =>
            {
                dt.ExportOptions = method.GetSchema<DataTable.Export>(context.Drill(nameof(DataTable.ExportOptions)));
                dt.FooterTemplate = method.GetSchema<DataTable.Footer>(context.Drill(nameof(DataTable.FooterTemplate)));
                dt.VirtualScrollerOptions = method.GetSchema<DataTable.VirtualScroller>(context.Drill(nameof(DataTable.VirtualScrollerOptions)));
                dt.ActionTemplate = method.GetSchema<DataTable.Column>(context.Drill(nameof(DataTable.ActionTemplate)));

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

        return B.DataTableExport(";", l($"{method.Name}.ExportFileName"), options: options);
    }

    public static DataTable.Footer MethodDataTableFooter(MethodModel method, ComponentContext context,
        Action<DataTable.Footer>? options = default
    )
    {
        var (_, l) = context;

        return B.DataTableFooter(l($"{method.Name}.FooterLabel"), options: options);
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

        return B.DataTableColumn(data.Prop,
            options: dtc =>
            {
                dtc.Component = property.GetRequiredComponent(context.Drill(nameof(DataTable.Column.Component)));

                options.Apply(dtc);
            }
        );
    }

    public static ComponentDescriptor<Conditional> PropertyConditional(PropertyModel property, ComponentContext context,
        Action<Conditional>? options = default
    )
    {
        context = context.Drill(nameof(Conditional));

        return B.Conditional(
            options: c =>
            {
                c.Fallback = property.GetRequiredComponent(context.Drill(nameof(Conditional.Fallback)));

                options.Apply(c);
            }
        );
    }

    public static ComponentDescriptor<Button> MethodButton(MethodModel method, ComponentContext context,
        Action<Button>? options = default
    )
    {
        var (_, l) = context;
        context = context.Drill(nameof(Button));

        return B.Button(l(method.Name.Humanize().Titleize()),
            action: method.GetSchema<RemoteAction>(context.Drill(nameof(IComponentDescriptor.Action))),
            options: options
        );
    }

    public static ComponentDescriptor<SimpleForm> MethodSimpleForm(MethodModel method, ComponentContext context,
        Action<SimpleForm>? options = default
    )
    {
        context = context.Drill(nameof(SimpleForm));
        var (_, l) = context;

        var submitButton =
            method.GetComponent<Button>(context.Drill(nameof(SimpleForm.SubmitButton))) as ComponentDescriptor<Button> ??
            MethodButton(method, context.Drill(nameof(SimpleForm.SubmitButton)));

        return B.SimpleForm(l(method.Name.Titleize()), submitButton.Schema,
            action: method.GetSchema<RemoteAction>(context.Drill(nameof(IComponentDescriptor.Action))),
            options: options
        );
    }
}