namespace Baked.Ui;

public class Modal(string Label)
    : IComponentSchema
{
    public IComponentDescriptor Content { get; set; } = default!;
    public IComponentDescriptor? Footer { get; set; }
    public IComponentDescriptor? Header { get; set; }
    public string Label { get; set; } = Label;
}