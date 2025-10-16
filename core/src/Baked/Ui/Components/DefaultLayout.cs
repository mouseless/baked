namespace Baked.Ui.Component;

public record DefaultLayout(string Path)
    : IGeneratedComponentSchema
{
    public string Path { get; set; } = Path;
    public IComponentDescriptor? SideMenu { get; set; }
    public IComponentDescriptor? Header { get; set; }
}