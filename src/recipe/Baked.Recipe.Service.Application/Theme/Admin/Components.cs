using Baked.Ui;

using static Baked.Theme.Admin.ErrorPage;

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
        bool collapsed = false
    ) => new(new(title, content) { Collapsed = collapsed });

    public static ComponentDescriptorAttribute<DefaultLayout> DefaultLayout(string name,
        IComponentDescriptor? sideMenu = default,
        IComponentDescriptor? header = default
    ) => new(new(name) { SideMenu = sideMenu, Header = header });

    public static ComponentDescriptorAttribute<ErrorPage> ErrorPage(
        IEnumerable<IComponentDescriptor>? links = default,
        Dictionary<int, ErrorInfo>? errorInfos = default,
        IData? data = default
    ) => new(new() { ErrorInfos = errorInfos ?? [], SafeLinks = [.. links ?? []] }) { Data = data };

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