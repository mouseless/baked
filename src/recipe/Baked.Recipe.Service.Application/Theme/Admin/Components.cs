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

    public static ComponentDescriptorAttribute<DefaultLayout> DefaultLayout(
        IComponentDescriptor? sideMenu = default,
        IComponentDescriptor? header = default
    ) => new(new() { SideMenu = sideMenu, Header = header });

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

    public static ComponentDescriptorAttribute<MenuPage> MenuPage(
        string? title = default,
        string? description = default,
        IEnumerable<IComponentDescriptor>? links = default
    ) => new(new() { Title = title, Description = description, Links = [.. links ?? []] });

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
        bool soon = false
    ) => new(route, icon) { Title = title, Soon = soon };

    public static ComponentDescriptor String(
        IData? data = default
    ) => new(nameof(String)) { Data = data };
}