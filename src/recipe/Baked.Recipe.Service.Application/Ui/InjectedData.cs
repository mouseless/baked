namespace Baked.Ui;

public record InjectedData(InjectedData.DataKey Key)
    : IData
{
    public string Type => "Injected";
    public DataKey Key { get; set; } = Key;
    public string? Prop { get; set; }

    public enum DataKey
    {
        Custom,
        ParentData
    }
}