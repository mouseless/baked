namespace Baked.Ui;

public record MultiSelect : ISelect, ILabeler
{
    public Label? Label { get; set; }
    public bool? LocalizeOptionLabels { get; set; }
    public int? MaxSelectedLabels { get; set; }
    public string? OptionLabel { get; set; }
    public string? OptionValue { get; set; }
    public bool? ShowClear { get; set; }
    public bool? ShowToggleAll { get; set; }
    public bool? Stateful { get; set; }
    public string? TargetProp { get; set; }
}