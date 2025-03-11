using Baked.Ui;

namespace Baked.Theme.Admin;

public record MenuPage : IComponentSchema
{
    public IComponentDescriptor? Header { get; set; }
    public List<IComponentDescriptor> Links { get; init; } = [];
}