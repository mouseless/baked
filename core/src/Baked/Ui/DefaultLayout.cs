namespace Baked.Ui;

public record DefaultLayout(string Path)
    : IGeneratedComponentSchema
{
    public string Path { get; set; } = Path;
    public IComponentDescriptor? SideMenu { get; set; }
    public IComponentDescriptor? Header { get; set; }
    public ScrollTop? ScrollTopOptions { get; set; }

    public record ScrollTop
    {
        public int? Threshold { get; set; }
        public string? Icon { get; set; }
    }
}
