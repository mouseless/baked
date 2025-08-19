using Baked.Ui;

namespace Baked.Theme.Admin;

public static class Components
{
    public static ComponentDescriptorAttribute<CardLink> CardLink(string route, string title,
        Action<CardLink>? schema = default
    ) => new(schema.Apply(new(route, title)));

    public static Conditional Conditional(
        Action<Conditional>? schema = default
    ) => schema.Apply(new());

    public static Conditional.Condition ConditionalCondition(string prop, object value, IComponentDescriptor component,
        Action<Conditional.Condition>? schema = default
    ) => schema.Apply(new(prop, value, component));

    public static ComponentDescriptor Custom<TSchema>() where TSchema : IComponentSchema =>
        new(typeof(TSchema).Name);

    public static ComponentDescriptor CustomPage<TSchema>(string path,
        string? layout = default
    ) where TSchema : IComponentSchema => new(typeof(TSchema).Name, schema: new CustomPage(path, layout));

    public static ComponentDescriptorAttribute<DataPanel> DataPanel(string title, IComponentDescriptor content,
        Action<DataPanel>? schema = default
    ) => DataPanel(Datas.Inline(title), content, schema: schema);

    public static ComponentDescriptorAttribute<DataPanel> DataPanel(IData title, IComponentDescriptor content,
        Action<DataPanel>? schema = default
    ) => new(schema.Apply(new(title, content)));

    public static ComponentDescriptorAttribute<DataTable> DataTable(
        Action<DataTable>? schema = default,
        IData? data = default
    ) => new(schema.Apply(new())) { Data = data };

    public static DataTable.Column DataTableColumn(string prop, IComponentDescriptor component,
        Action<DataTable.Column>? schema = default
    ) => DataTableColumn(prop, schema: s =>
    {
        s.Component = Conditional(schema: s => s.Fallback = component);
        schema.Apply(s);
    });

    public static DataTable.Column DataTableColumn(string prop,
        Action<DataTable.Column>? schema = default
    ) => schema.Apply(new(prop));

    public static DataTable.Export DataTableExport(string csvSeparator, string fileName,
        Action<DataTable.Export>? schema = default
    ) => schema.Apply(new(csvSeparator, fileName));

    public static DataTable.Footer DataTableFooter(string label,
        Action<DataTable.Footer>? schema = default
    ) => schema.Apply(new(label));

    public static DataTable.VirtualScroller DataTableVirtualScroller(
        Action<DataTable.VirtualScroller>? schema = default
    ) => schema.Apply(new());

    public static ComponentDescriptorAttribute<DefaultLayout> DefaultLayout(string name,
        Action<DefaultLayout>? schema = default
    ) => new(schema.Apply(new(name)));

    public static ComponentDescriptorAttribute<ErrorPage> ErrorPage(
        Action<ErrorPage>? schema = default,
        IData? data = default
    ) => new(schema.Apply(new())) { Data = data };

    public static ErrorPage.Info ErrorPageInfo(string title, string message,
        Action<ErrorPage.Info>? schema = default
    ) => schema.Apply(new(title, message));

    public static ComponentDescriptorAttribute<Filter> Filter(string pageContextKey,
        Action<Filter>? schema = default
    ) => new(schema.Apply(new(pageContextKey)));

    public static Filterable Filterable(IComponentDescriptor component,
        Action<Filterable>? schema = default
    ) => schema.Apply(new(component));

    public static ComponentDescriptorAttribute<Header> Header(
        Action<Header>? schema = default,
        IData? data = default
    ) => new(schema.Apply(new())) { Data = data ?? Datas.Computed(Composables.UseRoute) };

    public static Header.Item HeaderItem(string route,
        Action<Header.Item>? schema = default
    ) => schema.Apply(new(route));

    public static ComponentDescriptorAttribute<LanguageSwitcher> LanguageSwitcher(
        Action<LanguageSwitcher>? schema = default
    ) => new(schema.Apply(new()));

    public static ComponentDescriptorAttribute<Icon> Icon(string iconClass,
        Action<Icon>? schema = default
    ) => new(schema.Apply(new(iconClass)));

    public static ComponentDescriptorAttribute<MenuPage> MenuPage(string name, IEnumerable<IComponentDescriptor> links,
        Action<MenuPage>? schema = default
    ) => MenuPage(name,
        schema: s =>
        {
            s.Sections.Add(MenuPageSection(schema: s => s.Links.AddRange(links.Select(l => Filterable(l)))));
            schema.Apply(s);
        }
    );

    public static ComponentDescriptorAttribute<MenuPage> MenuPage(string name,
        Action<MenuPage>? schema = default
    ) => new(schema.Apply(new(name)));

    public static MenuPage.Section MenuPageSection(
        Action<MenuPage.Section>? schema = default
    ) => schema.Apply(new());

    public static ComponentDescriptorAttribute<Message> Message(
        Action<Message>? schema = default,
        string? data = default
    ) => Message(
        schema: schema,
        data: data is not null ? Datas.Inline(data) : null
    );

    public static ComponentDescriptorAttribute<Message> Message(
        Action<Message>? schema = default,
        IData? data = default
    ) => new(schema.Apply(new() { LocalizeMessage = data?.RequireLocalization ?? null })) { Data = data };

    public static ComponentDescriptorAttribute<ModalLayout> ModalLayout(string name,
        Action<ModalLayout>? schema = default
    ) => new(schema.Apply(new(name)));

    public static ComponentDescriptorAttribute<Money> Money(
        Action<Money>? schema = default,
        IData? data = default
    ) => new(schema.Apply(new())) { Data = data };

    public static ComponentDescriptorAttribute<NavLink> NavLink(string path, string idProp, string textProp,
        Action<NavLink>? schema = default
    ) => new(schema.Apply(new(path, idProp, textProp)));

    public static ComponentDescriptorAttribute<None> None(
        Action<None>? schema = default
    ) => new(schema.Apply(new()));

    public static ComponentDescriptorAttribute<Number> Number(
        Action<Number>? schema = default,
        IData? data = default
    ) => new(schema.Apply(new())) { Data = data };

    public static ComponentDescriptorAttribute<PageTitle> PageTitle(string title,
        Action<PageTitle>? schema = default
    ) => new(schema.Apply(new(title)));

    public static Parameter Parameter(string name, IComponentDescriptor component,
        Action<Parameter>? schema = default
    ) => schema.Apply(new(name, component));

    public static ComponentDescriptorAttribute<Rate> Rate(
        Action<Rate>? schema = default,
        IData? data = default
    ) => new(schema.Apply(new())) { Data = data };

    public static ComponentDescriptorAttribute<ReportPage> ReportPage(string name, ComponentDescriptorAttribute<PageTitle> title,
        Action<ReportPage>? schema = default
    ) => new(schema.Apply(new(name, title.Schema)));

    public static ReportPage.Tab ReportPageTab(string id,
        Action<ReportPage.Tab>? schema = default
    ) => schema.Apply(new(id));

    public static ReportPage.Tab.Content ReportPageTabContent(IComponentDescriptor component,
        Action<ReportPage.Tab.Content>? schema = default
    ) => schema.Apply(new(component));

    public static ComponentDescriptorAttribute<Select> Select(string label, IData data,
        Action<Select>? schema = default
    ) => new(schema.Apply(new(label) { LocalizeLabel = data.RequireLocalization })) { Data = data };

    public static ComponentDescriptorAttribute<SelectButton> SelectButton(IData data,
        Action<SelectButton>? schema = default
    ) => new(schema.Apply(new() { LocalizeLabel = data.RequireLocalization })) { Data = data };

    public static ComponentDescriptorAttribute<SideMenu> SideMenu(
        Action<SideMenu>? schema = default,
        IData? data = default
    ) => new(schema.Apply(new())) { Data = data ?? Datas.Computed(Composables.UseRoute) };

    public static SideMenu.Item SideMenuItem(string route, string icon,
        Action<SideMenu.Item>? schema = default
    ) => schema.Apply(new(route, icon));

    public static ComponentDescriptorAttribute<String> String(
        Action<String>? schema = default,
        IData? data = default
    ) => new(schema.Apply(new())) { Data = data };
}