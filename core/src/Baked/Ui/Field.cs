using B = Baked.Ui.Components;

namespace Baked.Ui;

public record Field(string Key, string Label)
{
    public string Key { get; set; } = Key;
    public string Label { get; set; } = Label;
    public IComponentDescriptor Component { get; set; } = B.Text();
    public bool? Wide { get; set; }
}