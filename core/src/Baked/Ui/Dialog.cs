namespace Baked.Ui;

public class Dialog(Button Open, string Header, IComponentDescriptor Content)
    : IComponentSchema
{
    public IComponentDescriptor Content { get; set; } = Content;
    public Button Open { get; set; } = Open;
    public string Header { get; set; } = Header;
    public Button? Submit { get; set; }
}