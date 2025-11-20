namespace Baked.Ui;

public record RemoteAction(string Path)
    : IAction
{
    public string Type => "Remote";
    public string Path { get; set; } = Path;
    public string Method { get; set; } = "Post";
    public IData? Headers { get; set; }
    public IData? Query { get; set; }
    public IData? Params { get; set; }
    public IData? Body { get; set; }
}