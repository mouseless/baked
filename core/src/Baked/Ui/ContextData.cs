using Newtonsoft.Json;

namespace Baked.Ui;

public record ContextData : IData
{
    public string Type => "Context";
    public DataKey Key { get; set; } = DataKey.ModelData;
    public string? Prop { get; set; }
    public string? TargetProp { get; set; }

    [JsonIgnore]
    public bool? RequireLocalization { get; set; }

    public enum DataKey
    {
        ParentData,
        ModelData
    }
}