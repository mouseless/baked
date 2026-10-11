namespace Baked.Ui;

public record RemoteAction : IAction
{
    public string Type => "Remote";
    public string Path { get; set; } = string.Empty;
    public string Method { get; set => field = value.ToUpperInvariant(); } = "POST";
    public IData? Headers { get; set; }
    public IData? Query { get; set; }
    public IData? Params { get; set; }
    public IData? Body { get; set; }
    public IAction PostAction { get; set; } = Execute.Publish.Event("remote-action-complete");
}