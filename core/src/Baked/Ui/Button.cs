namespace Baked.Ui;

public record Button(string Label)
    : IComponentSchema
{
    public string? Icon { get; set; }
    public string Label { get; set; } = Label;
    public string? Variant { get; set; }
    public bool? Rounded { get; set; }
}