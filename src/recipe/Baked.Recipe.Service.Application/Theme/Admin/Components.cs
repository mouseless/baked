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
        string? placeholder = default
    ) => new(new(pageContextKey) { Placeholder = placeholder });

    public static Filterable Filterable(string title, IComponentDescriptor component) =>
        new(title, component);

    public static ComponentDescriptorAttribute<Header> Header(IEnumerable<Header.Item> siteMap,
        IData? data = default
    )
    {
        data ??= Datas.Computed(Composables.UseRoute);

        return new(new() { Sitemap = siteMap.ToDictionary(i => i.Route, i => i) }) { Data = data };
    }

    public static Header.Item HeaderItem(string route,
        string? icon = default,
        string? title = default,
        string? parentRoute = default
    ) => new(route) { Icon = icon, Title = title, ParentRoute = parentRoute };

    public static ComponentDescriptor LanguageSwitcher() =>
        new(nameof(LanguageSwitcher));

    public static ComponentDescriptorAttribute<Icon> Icon(string iconClass) =>
        new(new(iconClass));

    public static ComponentDescriptorAttribute<MenuPage> MenuPage(string name,
        IComponentDescriptor? header = default,
        IEnumerable<IComponentDescriptor>? links = default
    ) => MenuPage(name,
        header: header,
        sections: [MenuPageSection(links: links?.Select(l => Filterable(string.Empty, l)))]
    );

    public static ComponentDescriptorAttribute<MenuPage> MenuPage(string name,
        IComponentDescriptor? header = default,
        IEnumerable<MenuPage.Section>? sections = default,
        string? filterPageContextKey = default
    ) => new(new(name) { Header = header, FilterPageContextKey = filterPageContextKey, Sections = [.. sections ?? []] });

    public static MenuPage.Section MenuPageSection(
        string? title = default,
        IEnumerable<Filterable>? links = default
    ) => new() { Title = title, Links = [.. links ?? []] };

    public static ComponentDescriptorAttribute<Message> Message(
        string? severity = default,
        string? icon = default,
        string? message = default
    ) => Message(
        severity: severity,
        icon: icon,
        data: message is not null ? Datas.Inline(message) : null
    );

    public static ComponentDescriptorAttribute<Message> Message(
        string? severity = default,
        string? icon = default,
        IData? data = default
    )
    {
        severity ??= "info";

        return new(new(severity) { Icon = icon, LocalizeMessage = data?.RequireLocalization ?? null }) { Data = data };
    }

    public static ComponentDescriptorAttribute<ModalLayout> ModalLayout(string name) =>
        new(new(name));

    public static ComponentDescriptor Money(
        IData? data = default
    ) => new(nameof(Money)) { Data = data };

    public static ComponentDescriptorAttribute<NavLink> NavLink(string path, string idProp, string textProp) =>
        new(new(path, idProp, textProp));

    public static ComponentDescriptor None() =>
        new(nameof(None));

    public static ComponentDescriptor Number(
        IData? data = default
    ) => new(nameof(Number)) { Data = data };

    public static ComponentDescriptorAttribute<PageTitle> PageTitle(string title,
        string? description = default,
        IEnumerable<IComponentDescriptor>? actions = default
    ) => new(new(title) { Description = description, Actions = [.. actions ?? []] });

    public static Parameter Parameter(string name, IComponentDescriptor component,
        bool? required = default,
        bool? defaultSelfManaged = default,
        IData? @default = default,
        object? defaultValue = default
    ) => new(name, component)
    {
        Required = required,
        DefaultSelfManaged = defaultSelfManaged,
        Default =
            @default is not null ? @default :
            defaultValue is not null ? Datas.Inline(defaultValue) :
            null
    };

    public static ComponentDescriptor Rate(
        IData? data = default
    ) => new(nameof(Rate)) { Data = data };

    public static ComponentDescriptorAttribute<ReportPage> ReportPage(string name, ComponentDescriptorAttribute<PageTitle> title,
        IEnumerable<Parameter>? queryParameters = default,
        IEnumerable<ReportPage.Tab>? tabs = default
    ) => new(new(name, title.Schema) { QueryParameters = [.. queryParameters ?? []], Tabs = [.. tabs ?? []] });

    public static ReportPage.Tab ReportPageTab(string id, string title,
        IEnumerable<ReportPage.Tab.Content>? contents = default,
        bool? fullScreen = default,
        IComponentDescriptor? icon = default,
        bool? overflow = default,
        string? showWhen = default
    ) => new(id, title) { Contents = [.. contents ?? []], FullScreen = fullScreen, Icon = icon, Overflow = overflow, ShowWhen = showWhen };

    public static ReportPage.Tab.Content ReportPageTabContent(IComponentDescriptor component,
        string? key = default,
        bool? narrow = default,
        string? showWhen = default
    ) => new(component) { Key = key, Narrow = narrow, ShowWhen = showWhen };

    public static ComponentDescriptorAttribute<Select> Select(string label, IData data,
        string? optionLabel = default,
        string? optionValue = default,
        bool? showClear = default,
        bool? stateful = default,
        string? selectionPageContextKey = default
    ) => new(new(label)
    {
        OptionLabel = optionLabel,
        OptionValue = optionValue,
        LocalizeLabel = data.RequireLocalization,
        ShowClear = showClear,
        Stateful = stateful,
        SelectionPageContextKey = selectionPageContextKey
    })
    { Data = data };

    public static ComponentDescriptorAttribute<SelectButton> SelectButton(IData data,
        bool? allowEmpty = default,
        string? optionLabel = default,
        string? optionValue = default,
        bool? stateful = default,
        string? selectionPageContextKey = default
    ) => new(new()
    {
        AllowEmpty = allowEmpty,
        OptionLabel = optionLabel,
        OptionValue = optionValue,
        LocalizeLabel = data.RequireLocalization,
        Stateful = stateful,
        SelectionPageContextKey = selectionPageContextKey
    })
    { Data = data };

    public static ComponentDescriptorAttribute<SideMenu> SideMenu(IEnumerable<SideMenu.Item> menu,
        IComponentDescriptor? footer = default,
        IData? data = default
    )
    {
        data ??= Datas.Computed(Composables.UseRoute);

        return new(new() { Menu = [.. menu], Footer = footer }) { Data = data };
    }

    public static SideMenu.Item SideMenuItem(string route, string icon,
        string? title = default,
        bool? disabled = default
    ) => new(route, icon) { Title = title, Disabled = disabled };

    public static ComponentDescriptorAttribute<String> String(
        int? maxLength = default,
        IData? data = default
    ) => new(new() { MaxLength = maxLength }) { Data = data };
}