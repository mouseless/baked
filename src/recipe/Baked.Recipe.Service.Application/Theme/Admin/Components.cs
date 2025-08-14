using Baked.Ui;

namespace Baked.Theme.Admin;

public static class Components
{
    public static ComponentDescriptorAttribute<CardLink> CardLink(string route, string title,
        string? icon = default,
        string? description = default,
        bool? disabled = default,
        string? disabledReason = default
    ) => new(new(route, title) { Icon = icon, Description = description, Disabled = disabled, DisabledReason = disabledReason });

    public static Conditional Conditional(
        IComponentDescriptor? fallback = default,
        IEnumerable<Conditional.Condition>? conditions = default
    ) => new(fallback ?? String()) { Conditions = [.. conditions ?? []] };

    public static Conditional.Condition ConditionalCondition(string prop, object value, IComponentDescriptor component) =>
        new(prop, value, component);

    public static ComponentDescriptor Custom<TSchema>() where TSchema : IComponentSchema =>
        new(typeof(TSchema).Name);

    public static ComponentDescriptor CustomPage<TSchema>(string path,
        string? layout = default
    ) where TSchema : IComponentSchema => new(typeof(TSchema).Name, schema: new CustomPage(path, layout));

    public static ComponentDescriptorAttribute<DataPanel> DataPanel(string title, IComponentDescriptor content,
        IEnumerable<Parameter>? parameters = default,
        bool? collapsed = default
    ) => DataPanel(Datas.Inline(title), content, parameters: parameters, collapsed: collapsed);

    public static ComponentDescriptorAttribute<DataPanel> DataPanel(IData title, IComponentDescriptor content,
        IEnumerable<Parameter>? parameters = default,
        bool? collapsed = default
    ) => new(new(title, content)
    {
        Collapsed = collapsed,
        LocalizeTitle = title.RequireLocalization,
        Parameters = [.. parameters ?? []]
    });

    public static ComponentDescriptorAttribute<DataTable> DataTable(
        IEnumerable<DataTable.Column>? columns = default,
        string? dataKey = default,
        string? itemsProp = default,
        bool? paginator = default,
        int? rows = default,
        int? rowsWhenLoading = default,
        string? scrollHeight = default,
        DataTable.VirtualScroller? virtualScrollerOptions = default,
        DataTable.Footer? footerTemplate = default,
        DataTable.Export? exportOptions = default,
        IData? data = default
    ) => new(
        new()
        {
            Columns = [.. columns ?? []],
            DataKey = dataKey,
            ItemsProp = itemsProp,
            Paginator = paginator,
            Rows = rows,
            RowsWhenLoading = rowsWhenLoading,
            ScrollHeight = scrollHeight,
            VirtualScrollerOptions = virtualScrollerOptions,
            FooterTemplate = footerTemplate,
            ExportOptions = exportOptions,
        }
    )
    { Data = data };

    public static DataTable.Column DataTableColumn(string prop, IComponentDescriptor component,
        string? title = default,
        bool? alignRight = default,
        bool? minWidth = default,
        bool? exportable = default,
        bool? frozen = default
    ) => DataTableColumn(prop,
        component: Conditional(fallback: component),
        title: title ?? " ", // otherwise export shows `label` as label
        alignRight: alignRight,
        minWidth: minWidth,
        exportable: exportable,
        frozen: frozen
    );

    public static DataTable.Column DataTableColumn(string prop,
        Conditional? component = default,
        string? title = default,
        bool? alignRight = default,
        bool? minWidth = default,
        bool? exportable = default,
        bool? frozen = default
    ) => new(prop, component ?? Conditional()) { AlignRight = alignRight, MinWidth = minWidth, Title = title, Exportable = exportable, Frozen = frozen };

    public static DataTable.Export DataTableExport(string csvSeparator, string fileName,
        string? formatter = default,
        string? buttonIcon = default,
        string? buttonLabel = default
    )
    {
        buttonIcon ??= "pi pi-download";

        return new(csvSeparator, fileName) { Formatter = formatter, ButtonIcon = buttonIcon, ButtonLabel = buttonLabel };
    }

    public static DataTable.Footer DataTableFooter(string label, List<DataTable.Column> columns) =>
        new(label) { Columns = columns };

    public static ComponentDescriptorAttribute<DefaultLayout> DefaultLayout(string name,
        IComponentDescriptor? sideMenu = default,
        IComponentDescriptor? header = default
    ) => new(new(name) { SideMenu = sideMenu, Header = header });

    public static DataTable.VirtualScroller DataTableVirtualScroller(int itemSize,
        int? numToleratedItems = 10,
        bool? appendOnly = true
    ) => new(itemSize) { NumToleratedItems = numToleratedItems, AppendOnly = appendOnly };

    public static ComponentDescriptorAttribute<ErrorPage> ErrorPage(
        IEnumerable<(int StatusCode, ErrorPage.Info Info)>? errorInfos = default,
        string? footerInfo = default,
        IEnumerable<IComponentDescriptor>? safeLinks = default,
        string? safeLinksMessage = default,
        IData? data = default
    ) => new(
        new(
            footerInfo ?? "If you cannot reach the page you want please contact the system administrator",
            safeLinksMessage ?? "Try the links from the menu below to view the page you want to access:"
        )
        { ErrorInfos = (errorInfos ?? []).ToDictionary(i => i.StatusCode, i => i.Info), SafeLinks = [.. safeLinks ?? []] })
    { Data = data };

    public static (int StatusCode, ErrorPage.Info Info) ErrorPageInfo(int statusCode, string title, string message) =>
        (statusCode, new(title, message));

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
        string? selectionContextKey = default
    ) => new(new(label)
    {
        OptionLabel = optionLabel,
        OptionValue = optionValue,
        LocalizeLabel = data.RequireLocalization,
        ShowClear = showClear,
        Stateful = stateful,
        SelectionContextKey = selectionContextKey
    })
    { Data = data };

    public static ComponentDescriptorAttribute<SelectButton> SelectButton(IData data,
        bool? allowEmpty = default,
        string? optionLabel = default,
        string? optionValue = default,
        bool? stateful = default,
        string? selectionContextKey = default
    ) => new(new()
    {
        AllowEmpty = allowEmpty,
        OptionLabel = optionLabel,
        OptionValue = optionValue,
        LocalizeLabel = data.RequireLocalization,
        Stateful = stateful,
        SelectionContextKey = selectionContextKey
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