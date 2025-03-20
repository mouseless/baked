using Baked.Ui;

namespace Baked.Theme.Admin;

public record Select(string Label)
    : IComponentSchema
{
    public string Label { get; set; } = Label;
    public string? OptionLabel { get; set; }
    public string? OptionValue { get; set; }
}