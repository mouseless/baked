namespace Baked.Ui;

public class Dialog(Button DialogButton, string Header, IComponentDescriptor Content)
    : IComponentSchema
{
    public Button? ActionButton { get; set; }
    public IComponentDescriptor Content { get; set; } = Content;
    public Button DialogButton { get; set; } = DialogButton;
    public string Header { get; set; } = Header;
}