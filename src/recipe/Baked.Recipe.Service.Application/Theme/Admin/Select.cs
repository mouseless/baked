using Baked.Ui;

namespace Baked.Theme.Admin;

public record Select(string Label)
    : IComponentSchema
{
    public string Label { get; set; } = Label;
    public string? OptionLabel { get; set; }
    public string? OptionValue { get; set; }
    public bool? LocalizeLabel { get; set; }
    public bool? ShowClear { get; set; }
    public bool? Stateful { get; set; }
}