namespace Baked.Ui;

public record DefaultLayout(string Path)
    : IGeneratedComponentSchema
{
    public string Path { get; set; } = Path;
    public IComponentDescriptor? SideMenu { get; set; }
    public IComponentDescriptor? Header { get; set; }
    public LayoutOptions? LayoutOptions { get; set; }
}

public record ScrollTopOptions
{
    public int? Threshold { get; set; }
    public string? Icon { get; set; }
}

public record LayoutOptions
{
    public ScrollTopOptions? ScrollTopOptions { get; set; }
}