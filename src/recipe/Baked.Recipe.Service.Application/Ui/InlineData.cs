using Newtonsoft.Json;

namespace Baked.Ui;

public record InlineData(object Value)
    : IData
{
    public string Type => "Inline";
    public object Value { get; set; } = Value;

    [JsonIgnore]
    public bool? RequireLocalization { get; set; } = true;
}