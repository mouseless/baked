using Newtonsoft.Json;

namespace Baked.Ui;

public record InlineData : IData
{
    public string Type => "Inline";
    public object Value { get; set; } = new();

    [JsonIgnore]
    public bool IsAsync => false;

    [JsonIgnore]
    public bool? RequireLocalization { get; set; } = true;
}