using Baked.Ui;

namespace Baked.Theme.Admin;

public record DefaultLayout : IComponentSchema
{
    public IComponentDescriptor? SideMenu { get; set; }
    public IComponentDescriptor? Header { get; set; }
}