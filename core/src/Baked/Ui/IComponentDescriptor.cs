namespace Baked.Ui;

public interface IComponentDescriptor
{
    string Type { get; }
    IComponentSchema Schema { get; }
    IData? Data { get; set; }
    public IAction? Action { get; set; }
    public Dictionary<string, Reaction>? When { get; set; }
}