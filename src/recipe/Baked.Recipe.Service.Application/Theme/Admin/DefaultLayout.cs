using Baked.Ui;

namespace Baked.Theme.Admin;

public record DefaultLayout(string Name)
    : INamedComponentSchema
{
    public string Name { get; set; } = Name;
    public IComponentDescriptor? SideMenu { get; set; }
    public IComponentDescriptor? Header { get; set; }
}