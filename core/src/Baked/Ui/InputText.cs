namespace Baked.Ui;

public record InputText(string Label)
    : IComponentSchema
{
    public string Label { get; set; } = Label;
    public string? TargetProp { get; set; }
}