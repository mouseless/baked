namespace Baked.Ui;

public record PublishAction : IAction
{
    public string Type => "Publish";
    public string? Event { get; set; }
    public string? PageContextKey { get; set; }
    public IData? Data { get; set; }
}