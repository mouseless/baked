using Newtonsoft.Json;

namespace Baked.Ui;

public record InjectedData : IData
{
    public string Type => "Injected";
    public DataKey Key { get; set; } = DataKey.Custom;
    public string? Prop { get; set; }

    [JsonIgnore]
    public bool? RequireLocalization { get; set; }

    public enum DataKey
    {
        Custom,
        ParentData
    }
}