namespace Baked.Ui;

public record SelectButton : ISelect, ILabeler
{
    public bool? AllowEmpty { get; set; }
    public bool? AutoSelectFirst { get; set; }
    public Labeler? Label { get; set; }
    public bool? LocalizeOptionLabels { get; set; }
    public string? OptionLabel { get; set; }
    public string? OptionValue { get; set; }
    public bool? Stateful { get; set; }
    public string? TargetProp { get; set; }
}