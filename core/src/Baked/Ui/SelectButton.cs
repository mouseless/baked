namespace Baked.Ui;

public record SelectButton : ISelect, IComponentSchema
{
    public bool? AllowEmpty { get; set; }
    public string? OptionLabel { get; set; }
    public string? OptionValue { get; set; }
    public bool? LocalizeLabel { get; set; }
    public bool? Stateful { get; set; }
    public string? TargetProp { get; set; }
}