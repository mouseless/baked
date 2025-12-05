namespace Baked.Ui;

public record EmitAction(string Event)
    : IAction
{
    public string Type => "Emit";
    public string Event { get; set; } = Event;
}