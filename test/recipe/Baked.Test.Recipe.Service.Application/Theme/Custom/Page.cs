using Baked.Theme.Admin;
using Baked.Ui;

using static Baked.Theme.Admin.Components;
using static Baked.Ui.UiLayer;

namespace Baked.Test.Theme.Custom;

public record Page(
    string Route,
    string Title
)
{
    public static Page CreateIndex(
        string? title = default,
        string? icon = default
    ) => CreateRoot("/", title ?? "Home", icon ?? "pi pi-home");

    public static Page CreateRoot(string route, string title, string icon) =>
        new(route, title) { Icon = icon, SideMenu = true, ErrorSafeLink = true };

    public static Page CreateChild(string route, string title, string parentRoute) =>
        new(route, title) { ParentRoute = parentRoute };

    public delegate IComponentDescriptor Builder(PageContext conext);

    public Builder? Build { get; set; }
    public string? Description { get; set; }
    public bool Disabled { get; set; }
    public string? DisabledReason { get; set; }
    public bool ErrorSafeLink { get; set; }
    public string? HeaderTitle { get; set; } = Route == "/" ? null : Title;
    public string? Icon { get; set; }
    public bool Index { get; set; } = Route == "/";
    public string Name { get; set; } = Route == "/" ? "index" : Route.TrimStart('/');
    public string? ParentRoute { get; set; }
    public string? Section { get; set; }
    public bool SideMenu { get; set; }
    public string? SideMenuTitle { get; set; } = Route == "/" ? null : Title;

    public IComponentDescriptor AsCardLink(NewLocaleKey l) =>
        CardLink(Route, l(Title), options: cl =>
        {
            cl.Icon = Icon;
            cl.Description = l(Description);
            cl.Disabled = Disabled ? true : null;
            cl.DisabledReason = l(DisabledReason);
        });

    public SideMenu.Item AsSideMenuItem(NewLocaleKey l) =>
        SideMenuItem(Route, Icon ?? throw new($"Icon is required for pages in side menu: `{Route}`"), options: smi =>
        {
            smi.Title = l(SideMenuTitle);
            smi.Disabled = Disabled ? true : null;
        });

    public Header.Item AsHeaderItem(NewLocaleKey l) =>
        HeaderItem(Route, options: hi =>
        {
            hi.Title = l(HeaderTitle);
            hi.Icon = Icon;
            hi.ParentRoute = ParentRoute;
        });
}