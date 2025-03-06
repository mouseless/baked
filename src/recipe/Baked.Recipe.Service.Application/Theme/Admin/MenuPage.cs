using Baked.Ui;

namespace Baked.Theme.Admin;

public record MenuPage : IComponentSchema
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<IComponentDescriptor> Links { get; init; } = [];
}