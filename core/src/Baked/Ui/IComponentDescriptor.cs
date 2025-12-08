namespace Baked.Ui;

public interface IComponentDescriptor : ISupportsReaction
{
    string Type { get; }
    IComponentSchema Schema { get; }
    IData? Data { get; set; }
    public IAction? Action { get; set; }
}