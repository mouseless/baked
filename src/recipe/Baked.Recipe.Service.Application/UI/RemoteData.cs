namespace Baked.Ui;

public class RemoteData : IData
{
    public string Type => "Remote";
    public required string Path { get; set; }
}