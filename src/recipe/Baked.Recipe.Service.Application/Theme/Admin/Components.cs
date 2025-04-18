using Baked.Ui;

namespace Baked.Theme.Admin;

public static class Components
{
    public static ComponentDescriptorAttribute<CardLink> CardLink(string route, string title,
        string? icon = default,
        string? description = default,
        bool disabled = false,
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
        bool collapsed = false
    ) => DataPanel(Datas.Inline(title), content, parameters: parameters, collapsed: collapsed);

    public static ComponentDescriptorAttribute<DataPanel> DataPanel(IData title, IComponentDescriptor content,
        IEnumerable<Parameter>? parameters = default,
        bool collapsed = false
    ) => new(new(title, content) { Collapsed = collapsed, Parameters = [.. parameters ?? []] });

    public static ComponentDescriptorAttribute<DataTable> DataTable(
        IEnumerable<DataTable.Column>? columns = default,
        string? dataKey = default,
        bool paginator = false,
        int? rows = default,
        int? rowsWhenLoading = default,
        string? scrollHeight = default,
        DataTable.FooterRow? footer = default,
        IData? data = default
    ) => new(new() { Columns = [.. columns ?? []], DataKey = dataKey, Paginator = paginator, Rows = rows, RowsWhenLoading = rowsWhenLoading, ScrollHeight = scrollHeight, Footer = footer }) { Data = data };

    public static DataTable.Column DataTableColumn(string prop, IComponentDescriptor component,
        string? title = default,
        bool minWidth = false
    ) => DataTableColumn(prop, component: Conditional(fallback: component), title: title, minWidth: minWidth);

    public static DataTable.Column DataTableColumn(string prop,
        Conditional? component = default,
        string? title = default,
        bool minWidth = false
    ) => new(prop, component ?? Conditional()) { MinWidth = minWidth, Title = title };

    public static DataTable.FooterRow DataTableFooter(string label, List<DataTable.FooterColumn> columns) =>
        new(label) { Columns = columns };

    public static DataTable.FooterColumn DataTableFooterColumn(string prop,
        Conditional? component = default
    ) => new(prop, component ?? Conditional());

    public static ComponentDescriptorAttribute<DefaultLayout> DefaultLayout(string name,
        IComponentDescriptor? sideMenu = default,
        IComponentDescriptor? header = default
    ) => new(new(name) { SideMenu = sideMenu, Header = header });

    public static ComponentDescriptorAttribute<ErrorPage> ErrorPage(
        IEnumerable<(int StatusCode, ErrorPage.Info Info)>? errorInfos = default,
        string? footerInfo = default,
        IEnumerable<IComponentDescriptor>? safeLinks = default,
        string? safeLinksMessage = default,
        IData? data = default
    ) => new(
        new(
            footerInfo ?? "If you cannot reach the page you want, please contact the system administrator.",
            safeLinksMessage ?? "Try the links from the menu below to view the page you want to access."
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

        return new(new(severity) { Icon = icon }) { Data = data };
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

    public static ComponentDescriptorAttribute<PageTitle> PageTitle(string title,
        string? description = default,
        IEnumerable<IComponentDescriptor>? actions = default
    ) => new(new(title) { Description = description, Actions = [.. actions ?? []] });

    public static Parameter Parameter(string name, IComponentDescriptor component,
        bool required = false,
        bool defaultSelfManaged = false,
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
        IComponentDescriptor? icon = default,
        string? showWhen = default,
        IEnumerable<ReportPage.Tab.Content>? contents = default
    ) => new(id, title) { Icon = icon, ShowWhen = showWhen, Contents = [.. contents ?? []] };

    public static ReportPage.Tab.Content ReportPageTabContent(IComponentDescriptor component,
        bool fullScreen = false,
        bool narrow = false,
        string? key = default,
        string? showWhen = default
    ) => new(component) { FullScreen = fullScreen, Narrow = narrow, Key = key, ShowWhen = showWhen };

    public static ComponentDescriptorAttribute<Select> Select(string label, IData data,
        string? optionLabel = default,
        string? optionValue = default,
        bool showClear = false,
        bool stateful = false
    ) => new(new(label) { OptionLabel = optionLabel, OptionValue = optionValue, ShowClear = showClear, Stateful = stateful }) { Data = data };

    public static ComponentDescriptorAttribute<SelectButton> SelectButton(IData data,
        bool allowEmpty = false,
        string? optionLabel = default,
        string? optionValue = default,
        bool stateful = false
    ) => new(new() { AllowEmpty = allowEmpty, OptionLabel = optionLabel, OptionValue = optionValue, Stateful = stateful }) { Data = data };

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
        bool disabled = false
    ) => new(route, icon) { Title = title, Disabled = disabled };

    public static ComponentDescriptorAttribute<String> String(
        int? maxLength = default,
        IData? data = default
    ) => new(new() { MaxLength = maxLength }) { Data = data };
}