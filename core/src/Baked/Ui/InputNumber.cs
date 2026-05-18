namespace Baked.Ui;

public record InputNumber : ILabeler, IComponentSchema
{
    public string? Label { get; set; }
    public string? LabelMode { get; set; }
    public string? LabelVariant { get; set; }
    public bool? ValidateLabel { get; set; }
    public bool? NoGrouping { get; set; }
}