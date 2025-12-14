namespace Baked.Ui;

public record EmitAction : IAction
{
    public string Type => "Emit";
    public string? Event { get; set; }
    public string? PageContextKey { get; set; }
    public IData? Data { get; set; }
}