using Newtonsoft.Json;

namespace Baked.Ui;

public record ContextData
    : IData
{
    public string Type => "Context";
    public required string Key { get; set; }
    public string? Prop { get; set; }
    public string? TargetProp { get; set; }

    [JsonIgnore]
    public bool IsAsync => false;

    [JsonIgnore]
    public bool? RequireLocalization { get; set; }
}