namespace Baked.UI;

public class RemoteData : IData
{
    public string Type => "Remote";
    public string Path { get; set; } = default!;
}