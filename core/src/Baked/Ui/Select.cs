namespace Baked.Ui;

public record Select : ISelect, ILabeler
{
    public bool? Filter { get; set; }
    public Label? Label { get; set; }
    public bool? LocalizeOptionLabels { get; set; }
    public string? OptionLabel { get; set; }
    public string? OptionValue { get; set; }
    public bool? ShowClear { get; set; }
    public bool? Stateful { get; set; }
    public string? TargetProp { get; set; }
}