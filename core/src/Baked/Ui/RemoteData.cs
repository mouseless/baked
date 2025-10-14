using Newtonsoft.Json;

namespace Baked.Ui;

public record RemoteData(string Path)
    : IData
{
    public string Type => "Remote";
    public string Path { get; set; } = Path;
    public IData? Headers { get; set; }
    public IData? Query { get; set; }
    public Dictionary<string, string>? Attributes { get; private set; }

    [JsonIgnore]
    public bool? RequireLocalization { get; set; }

    public void SetAttribute(string key, string value)
    {
        if (Attributes is null) { Attributes = []; }

        Attributes[key] = value;
    }
}