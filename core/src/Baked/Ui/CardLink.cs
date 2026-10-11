namespace Baked.Ui;

public record CardLink : IComponentSchema
{
    public string Route { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool? Disabled { get; set; }
    public string? DisabledReason { get; set; }
}