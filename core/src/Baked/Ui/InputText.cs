namespace Baked.Ui;

public record InputText : IComponentSchema, ILabeler
{
    public string? Label { get; set; }
    public string? LabelMode { get; set; }
    public string? LabelVariant { get; set; }
    public bool? ValidateLabel { get; set; }
    public string? TargetProp { get; set; }
}