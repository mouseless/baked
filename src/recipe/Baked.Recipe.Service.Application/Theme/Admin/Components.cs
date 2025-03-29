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

    public static ComponentDescriptorAttribute<DataPanel> DataPanel(string title, IComponentDescriptor content,
        IEnumerable<Parameter>? parameters = default,
        bool collapsed = false
    ) => new(new(title, content) { Collapsed = collapsed, Parameters = [.. parameters ?? []] });

    public static ComponentDescriptorAttribute<DataTable> DataTable(
        IEnumerable<DataTable.Column>? columns = default,
        int? rowCountWhenLoading = default,
        IData? data = default
    ) => new(new() { Columns = [.. columns ?? []], RowCountWhenLoading = rowCountWhenLoading }) { Data = data };

    public static DataTable.Column DataTableColumn(string prop, string title,
        IComponentDescriptor? component = default,
        bool minWidth = false
    ) => new(prop, title, component ?? String()) { MinWidth = minWidth };

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

    public static ComponentDescriptor Money(
        IData? data = default
    ) => new(nameof(Money)) { Data = data };

    public static ComponentDescriptorAttribute<MenuPage> MenuPage(string name,
        IComponentDescriptor? header = default,
        IEnumerable<IComponentDescriptor>? links = default
    ) => new(new(name) { Header = header, Links = [.. links ?? []] });

    public static ComponentDescriptor None() =>
        new(nameof(None));

    public static ComponentDescriptorAttribute<PageTitle> PageTitle(string title,
        string? description = default,
        IEnumerable<IComponentDescriptor>? actions = default
    ) => new(new(title) { Description = description, Actions = [.. actions ?? []] });

    public static Parameter Parameter(string name, IComponentDescriptor component,
        bool required = false,
        object? @default = default
    ) => new(name, component) { Required = required, Default = @default };

    public static ComponentDescriptor Rate(
        IData? data = default
    ) => new(nameof(Rate)) { Data = data };

    public static ComponentDescriptorAttribute<ReportPage> ReportPage(string name, ComponentDescriptorAttribute<PageTitle> title,
        IEnumerable<Parameter>? queryParameters = default,
        IEnumerable<ReportPage.Tab>? tabs = default
    ) => new(new(name, title.Schema) { QueryParameters = [.. queryParameters ?? []], Tabs = [.. tabs ?? []] });

    public static ReportPage.Tab ReportPageTab(string id, string title,
        IComponentDescriptor? icon = default,
        IEnumerable<ReportPage.Tab.Content>? contents = default
    ) => new(id, title) { Icon = icon, Contents = [.. contents ?? []] };

    public static ReportPage.Tab.Content ReportPageTabContent(IComponentDescriptor component,
        bool fullScreen = false,
        bool narrow = false
    ) => new(component) { FullScreen = fullScreen, Narrow = narrow };

    public static ComponentDescriptorAttribute<Select> Select(string label, IData data,
        string? optionLabel = default,
        string? optionValue = default,
        bool showClear = false
    ) => new(new(label) { OptionLabel = optionLabel, OptionValue = optionValue, ShowClear = showClear }) { Data = data };

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

    public static ComponentDescriptor String(
        IData? data = default
    ) => new(nameof(String)) { Data = data };
}