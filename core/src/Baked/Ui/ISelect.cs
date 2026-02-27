namespace Baked.Ui;

public interface ISelect : IComponentSchema
{
    string? OptionLabel { get; set; }
    string? OptionValue { get; set; }
    bool? LocalizeLabel { get; set; }
    bool? Stateful { get; set; }
    string? TargetProp { get; set; }
}