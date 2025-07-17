namespace Baked.Ui;

public record RemoteData(string Path)
    : IData
{
    public string Type => "Remote";
    public string Path { get; set; } = Path;
    public IData? Headers { get; set; }
    public IData? Query { get; set; }
    internal bool? RequireLocalization { get; set; }

    bool? IData.RequireLocalization => RequireLocalization;
}