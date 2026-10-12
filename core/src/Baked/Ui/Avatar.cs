namespace Baked.Ui;

public record Avatar : IComponentSchema
{
    public string? Icon { get; set; }
    public string? Size { get; set; }
}