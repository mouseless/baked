using Baked.Ui;

namespace Baked.Theme.Default;

public record DefaultLayout(string Path)
    : IGeneratedComponentSchema
{
    public string Path { get; set; } = Path;
    public IComponentDescriptor? SideMenu { get; set; }
    public IComponentDescriptor? Header { get; set; }
}