namespace Baked.Ui;

public record Button(string Label, IAction Action, IAction PostAction)
    : IComponentSchema
{
    public IAction Action { get; set; } = Action;
    public IAction PostAction { get; set; } = PostAction;
    public string? Icon { get; set; }
    public string Label { get; set; } = Label;
}