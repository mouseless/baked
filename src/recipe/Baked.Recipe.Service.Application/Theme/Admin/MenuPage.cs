using Baked.Ui;

namespace Baked.Theme.Admin;

public record MenuPage(string Name)
    : INamedComponentSchema
{
    public string Name { get; set; } = Name;
    public IComponentDescriptor? Header { get; set; }
    public List<IComponentDescriptor> Links { get; init; } = [];
}