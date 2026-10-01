namespace Baked.Ui;

public interface ISelect : IComponentSchema
{
    bool? AutoSelectFirst { get; set; }
    bool? LocalizeOptionLabels { get; set; }
    string? OptionLabel { get; set; }
    string? OptionValue { get; set; }
    bool? Stateful { get; set; }
    string? TargetProp { get; set; }
}