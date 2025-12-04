namespace Baked.Ui;

public interface IComponentDescriptor
{
    string Type { get; }
    IComponentSchema Schema { get; }
    IData? Data { get; set; }
    public IAction? Action { get; set; }
    public IAction? PostAction { get; set; }
    // TODO - review this in form components
    public Reaction? Reaction { get; set; }
}