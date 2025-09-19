using Baked.Ui;

namespace Baked.Theme.Default;

public record CardLink(string Route, string Title)
    : IComponentSchema
{
    public string Route { get; set; } = Route;
    public string? Icon { get; set; }
    public string Title { get; set; } = Title;
    public string? Description { get; set; }
    public bool? Disabled { get; set; }
    public string? DisabledReason { get; set; }
}