namespace Baked.Ui;

public record RemoteData(string Path)
    : IData
{
    public string Type => "Remote";
    public string Path { get; set; } = Path;
}