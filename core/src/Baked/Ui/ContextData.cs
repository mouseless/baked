using Newtonsoft.Json;

namespace Baked.Ui;

public record ContextData(string Key)
    : IData
{
    public string Type => "Context";
    public string Key { get; set; } = Key;
    public string? Prop { get; set; }
    public string? TargetProp { get; set; }

    [JsonIgnore]
    public bool IsAsync => false;

    [JsonIgnore]
    public bool? RequireLocalization { get; set; }
}