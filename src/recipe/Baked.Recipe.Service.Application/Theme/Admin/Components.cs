using Baked.Ui;

namespace Baked.Theme.Admin;

public static class Components
{
    public static ComponentDescriptorAttribute<CardLink> CardLink(string route, string title,
        Action<CardLink>? options = default
    ) => new(options.Apply(new(route, title)));

    public static Conditional Conditional(
        Action<Conditional>? options = default
    ) => options.Apply(new());

    public static Conditional.Condition ConditionalCondition(string prop, object value, IComponentDescriptor component,
        Action<Conditional.Condition>? options = default
    ) => options.Apply(new(prop, value, component));

    public static ComponentDescriptorAttribute<DataPanel> DataPanel(string title, IComponentDescriptor content,
        Action<DataPanel>? options = default
    ) => DataPanel(Datas.Inline(title), content, options: options);

    public static ComponentDescriptorAttribute<DataPanel> DataPanel(IData title, IComponentDescriptor content,
        Action<DataPanel>? options = default
    ) => new(options.Apply(new(title, content)));

    public static ComponentDescriptorAttribute<DataTable> DataTable(
        Action<DataTable>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data };

    public static DataTable.Column DataTableColumn(string prop, IComponentDescriptor component,
        Action<DataTable.Column>? options = default
    ) => DataTableColumn(prop, options: s =>
    {
        s.Component = Conditional(options: s => s.Fallback = component);
        options.Apply(s);
    });

    public static DataTable.Column DataTableColumn(string prop,
        Action<DataTable.Column>? options = default
    ) => options.Apply(new(prop));

    public static DataTable.Export DataTableExport(string csvSeparator, string fileName,
        Action<DataTable.Export>? options = default
    ) => options.Apply(new(csvSeparator, fileName));

    public static DataTable.Footer DataTableFooter(string label,
        Action<DataTable.Footer>? options = default
    ) => options.Apply(new(label));

    public static DataTable.VirtualScroller DataTableVirtualScroller(
        Action<DataTable.VirtualScroller>? options = default
    ) => options.Apply(new());

    public static ComponentDescriptorAttribute<DefaultLayout> DefaultLayout(string name,
        Action<DefaultLayout>? options = default
    ) => new(options.Apply(new(name)));

    public static ComponentDescriptorAttribute<ErrorPage> ErrorPage(
        Action<ErrorPage>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data };

    public static ErrorPage.Info ErrorPageInfo(string title, string message,
        Action<ErrorPage.Info>? options = default
    ) => options.Apply(new(title, message));

    public static ComponentDescriptorAttribute<Filter> Filter(string pageContextKey,
        Action<Filter>? options = default
    ) => new(options.Apply(new(pageContextKey)));

    public static Filterable Filterable(IComponentDescriptor component,
        Action<Filterable>? options = default
    ) => options.Apply(new(component));

    public static ComponentDescriptorAttribute<Header> Header(
        Action<Header>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data ?? Datas.Computed(Composables.UseRoute) };

    public static Header.Item HeaderItem(string route,
        Action<Header.Item>? options = default
    ) => options.Apply(new(route));

    public static ComponentDescriptorAttribute<LanguageSwitcher> LanguageSwitcher(
        Action<LanguageSwitcher>? options = default
    ) => new(options.Apply(new()));

    public static ComponentDescriptorAttribute<Icon> Icon(string iconClass,
        Action<Icon>? options = default
    ) => new(options.Apply(new(iconClass)));

    public static ComponentDescriptorAttribute<MenuPage> MenuPage(string name, IEnumerable<IComponentDescriptor> links,
        Action<MenuPage>? options = default
    ) => MenuPage(name,
        options: s =>
        {
            s.Sections.Add(MenuPageSection(options: s => s.Links.AddRange(links.Select(l => Filterable(l)))));
            options.Apply(s);
        }
    );

    public static ComponentDescriptorAttribute<MenuPage> MenuPage(string name,
        Action<MenuPage>? options = default
    ) => new(options.Apply(new(name)));

    public static MenuPage.Section MenuPageSection(
        Action<MenuPage.Section>? options = default
    ) => options.Apply(new());

    public static ComponentDescriptorAttribute<Message> Message(
        Action<Message>? options = default,
        string? data = default
    ) => Message(
        options: options,
        data: data is not null ? Datas.Inline(data) : null
    );

    public static ComponentDescriptorAttribute<Message> Message(
        Action<Message>? options = default,
        IData? data = default
    ) => new(options.Apply(new() { LocalizeMessage = data?.RequireLocalization ?? null })) { Data = data };

    public static ComponentDescriptorAttribute<ModalLayout> ModalLayout(string name,
        Action<ModalLayout>? options = default
    ) => new(options.Apply(new(name)));

    public static ComponentDescriptorAttribute<Money> Money(
        Action<Money>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data };

    public static ComponentDescriptorAttribute<NavLink> NavLink(string path, string idProp, string textProp,
        Action<NavLink>? options = default
    ) => new(options.Apply(new(path, idProp, textProp)));

    public static ComponentDescriptorAttribute<None> None(
        Action<None>? options = default
    ) => new(options.Apply(new()));

    public static ComponentDescriptorAttribute<Number> Number(
        Action<Number>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data };

    public static ComponentDescriptorAttribute<PageTitle> PageTitle(string title,
        Action<PageTitle>? options = default
    ) => new(options.Apply(new(title)));

    public static Parameter Parameter(string name, IComponentDescriptor component,
        Action<Parameter>? options = default
    ) => options.Apply(new(name, component));

    public static ComponentDescriptorAttribute<Rate> Rate(
        Action<Rate>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data };

    public static ComponentDescriptorAttribute<ReportPage> ReportPage(string name, ComponentDescriptorAttribute<PageTitle> title,
        Action<ReportPage>? options = default
    ) => new(options.Apply(new(name, title.Schema)));

    public static ReportPage.Tab ReportPageTab(string id,
        Action<ReportPage.Tab>? options = default
    ) => options.Apply(new(id));

    public static ReportPage.Tab.Content ReportPageTabContent(IComponentDescriptor component,
        Action<ReportPage.Tab.Content>? options = default
    ) => options.Apply(new(component));

    public static ComponentDescriptorAttribute<Select> Select(string label, IData data,
        Action<Select>? options = default
    ) => new(options.Apply(new(label) { LocalizeLabel = data.RequireLocalization })) { Data = data };

    public static ComponentDescriptorAttribute<SelectButton> SelectButton(IData data,
        Action<SelectButton>? options = default
    ) => new(options.Apply(new() { LocalizeLabel = data.RequireLocalization })) { Data = data };

    public static ComponentDescriptorAttribute<SideMenu> SideMenu(
        Action<SideMenu>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data ?? Datas.Computed(Composables.UseRoute) };

    public static SideMenu.Item SideMenuItem(string route, string icon,
        Action<SideMenu.Item>? options = default
    ) => options.Apply(new(route, icon));

    public static ComponentDescriptorAttribute<String> String(
        Action<String>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data };
}