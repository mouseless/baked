using Baked.Ui;

namespace Baked.Theme;

public record Route(
    string Path,
    string Title
)
{
    public Func<Page, PageBuilder?> Page { get; set; } = p => p.Implemented();
    public string? Description { get; set; }
    public bool Disabled { get; set; }
    public string? DisabledReason { get; set; }
    public bool ErrorSafeLink { get; set; }
    public string? HeaderTitle { get; set; } = Path == "/" ? null : Title;
    public string? Icon { get; set; }
    public bool Index { get; set; } = Path == "/";
    public string Name { get; set; } = Path == "/" ? "index" : Path.TrimStart('/');
    public string? ParentPath { get; set; }
    public bool Parameterized { get; set; } = false;
    public string? Section { get; set; }
    public bool SideMenu { get; set; }
    public string? SideMenuTitle { get; set; } = Title;

    public IComponentDescriptor? BuildPage(PageContext context) =>
        Page(new())?.Invoke(context);
}