using Baked.Ui;

namespace Baked.Theme.Default;

public record Filterable(IComponentDescriptor Component)
    : IComponentSchema
{
    public string Title { get; set; } = string.Empty;
    public IComponentDescriptor Component { get; set; } = Component;
}