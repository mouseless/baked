namespace Baked.Ui;

public static class Components
{
    public static ComponentDescriptor<Button> Button(string label, IAction action,
        Action<Button>? options = default
    ) => new(options.Apply(new(label))) { Action = action };

    public static ComponentDescriptor<CardLink> CardLink(string route, string title,
        Action<CardLink>? options = default
    ) => new(options.Apply(new(route, title)));

    public static ComponentDescriptor<Conditional> Conditional(
        Action<Conditional>? options = default
    ) => new(options.Apply(new()));

    public static Conditional.Condition ConditionalCondition(string prop, object value, IComponentDescriptor component,
        Action<Conditional.Condition>? options = default
    ) => options.Apply(new(prop, value, component));

    public static ComponentDescriptor<DataPanel> DataPanel(string title, IComponentDescriptor content,
        Action<DataPanel>? options = default
    ) => DataPanel(Datas.Inline(title), content, options: options);

    public static ComponentDescriptor<DataPanel> DataPanel(IData title, IComponentDescriptor content,
        Action<DataPanel>? options = default
    ) => new(options.Apply(new(title, content)));

    public static ComponentDescriptor<DataTable> DataTable(
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

    public static ComponentDescriptor<DefaultLayout> DefaultLayout(string name,
        Action<DefaultLayout>? options = default
    ) => new(options.Apply(new(name)));

    public static DefaultLayout.ScrollTop DefaultLayoutScrollTop(
        Action<DefaultLayout.ScrollTop>? options = default
    ) => options.Apply(new());

    public static ComponentDescriptor<ErrorPage> ErrorPage(
        Action<ErrorPage>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data };

    public static ErrorPage.Info ErrorPageInfo(string title, string message,
        Action<ErrorPage.Info>? options = default
    ) => options.Apply(new(title, message));

    public static ComponentDescriptor<Filter> Filter(string pageContextKey,
        Action<Filter>? options = default
    ) => new(options.Apply(new(pageContextKey)));

    public static Filterable Filterable(IComponentDescriptor component,
        Action<Filterable>? options = default
    ) => options.Apply(new(component));

    public static ComponentDescriptor<Header> Header(
        Action<Header>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data ?? Datas.Computed.UseRoute() };

    public static Header.Item HeaderItem(string route,
        Action<Header.Item>? options = default
    ) => options.Apply(new(route));

    public static ComponentDescriptor<Icon> Icon(string iconClass,
        Action<Icon>? options = default
    ) => new(options.Apply(new(iconClass)));

    public static ComponentDescriptor<LanguageSwitcher> LanguageSwitcher(
        Action<LanguageSwitcher>? options = default
    ) => new(options.Apply(new()));

    public static ComponentDescriptor<MenuPage> MenuPage(string path, IEnumerable<IComponentDescriptor> links,
        Action<MenuPage>? options = default
    ) => MenuPage(path,
        options: s =>
        {
            s.Sections.Add(MenuPageSection(options: s => s.Links.AddRange(links.Select(l => Filterable(l)))));
            options.Apply(s);
        }
    );

    public static ComponentDescriptor<MenuPage> MenuPage(string path,
        Action<MenuPage>? options = default
    ) => new(options.Apply(new(path)));

    public static MenuPage.Section MenuPageSection(
        Action<MenuPage.Section>? options = default
    ) => options.Apply(new());

    public static ComponentDescriptor<Message> Message(
        Action<Message>? options = default,
        string? data = default
    ) => Message(
        options: options,
        data: data is not null ? Datas.Inline(data) : null
    );

    public static ComponentDescriptor<Message> Message(
        Action<Message>? options = default,
        IData? data = default
    ) => new(options.Apply(new() { LocalizeMessage = data?.RequireLocalization })) { Data = data };

    public static ComponentDescriptor<ModalLayout> ModalLayout(string name,
        Action<ModalLayout>? options = default
    ) => new(options.Apply(new(name)));

    public static ComponentDescriptor<MissingComponent> MissingComponent(
        Action<MissingComponent>? options = default
    ) => new(options.Apply(new()));

    public static MissingComponent.DomainSource MissingComponentDomainSource(string type,
        Action<MissingComponent.DomainSource>? options = default
    ) => options.Apply(new(type));

    public static ComponentDescriptor<Money> Money(
        Action<Money>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data };

    public static ComponentDescriptor<NavLink> NavLink(string path, string idProp, string textProp,
        Action<NavLink>? options = default
    ) => new(options.Apply(new(path, idProp, textProp)));

    public static ComponentDescriptor<Number> Number(
        Action<Number>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data };

    public static ComponentDescriptor<PageTitle> PageTitle(string title,
        Action<PageTitle>? options = default
    ) => new(options.Apply(new(title)));

    public static Input Input(string name, IComponentDescriptor component,
        Action<Input>? options = default
    ) => options.Apply(new(name, component));

    public static ComponentDescriptor<Rate> Rate(
        Action<Rate>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data };

    public static ComponentDescriptor<ReportPage> ReportPage(string path, ComponentDescriptor<PageTitle> title,
        Action<ReportPage>? options = default
    ) => new(options.Apply(new(path, title.Schema)));

    public static ReportPage.Tab ReportPageTab(string id,
        Action<ReportPage.Tab>? options = default
    ) => options.Apply(new(id));

    public static ReportPage.Tab.Content ReportPageTabContent(IComponentDescriptor component, string key,
        Action<ReportPage.Tab.Content>? options = default
    ) => options.Apply(new(component, key));

    public static ComponentDescriptor<Select> Select(string label, IData data,
        Action<Select>? options = default
    ) => new(options.Apply(new(label) { LocalizeLabel = data.RequireLocalization })) { Data = data };

    public static ComponentDescriptor<SelectButton> SelectButton(IData data,
        Action<SelectButton>? options = default
    ) => new(options.Apply(new() { LocalizeLabel = data.RequireLocalization })) { Data = data };

    public static ComponentDescriptor<SideMenu> SideMenu(
        Action<SideMenu>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data ?? Datas.Computed.UseRoute() };

    public static SideMenu.Item SideMenuItem(string route, string icon,
        Action<SideMenu.Item>? options = default
    ) => options.Apply(new(route, icon));

    public static ComponentDescriptor<SimpleForm> SimpleForm(
        IAction? action = default,
        Action<SimpleForm>? options = default
    ) => new(options.Apply(new())) { Action = action };

    public static ComponentDescriptor<Text> Text(
        Action<Text>? options = default,
        IData? data = default
    ) => new(options.Apply(new())) { Data = data };
}