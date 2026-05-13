namespace Baked.Ui;

public record Select : ISelect, ILabeler
{
    public bool? Filter { get; set; }
    public string? Label { get; set; }
    public string? LabelMode { get; set; }
    public string? LabelVariant { get; set; }
    public bool? ValidateLabel { get; set; }
    public string? OptionLabel { get; set; }
    public string? OptionValue { get; set; }
    public bool? LocalizeLabel { get; set; }
    public bool? ShowClear { get; set; }
    public bool? Stateful { get; set; }
    public string? TargetProp { get; set; }
}