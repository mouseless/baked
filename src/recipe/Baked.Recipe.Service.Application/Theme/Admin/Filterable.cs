using Baked.Ui;

namespace Baked.Theme.Admin;

public record Filterable(IComponentDescriptor Component)
    : IComponentSchema
{
    public string Title { get; set; } = string.Empty;
    public IComponentDescriptor Component { get; set; } = Component;
}