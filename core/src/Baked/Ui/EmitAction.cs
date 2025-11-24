namespace Baked.Ui;

public record EmitAction(string EventKey)
    : IAction
{
    public string Type => "Emit";
    public string EventKey { get; set; } = EventKey!;
}