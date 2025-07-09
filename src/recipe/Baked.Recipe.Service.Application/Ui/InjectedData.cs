namespace Baked.Ui;

public record InjectedData(InjectedData.DataKey Key)
    : IData
{
    public string Type => "Injected";
    public DataKey Key { get; set; } = Key;
    public string? Prop { get; set; }
    internal bool? RequireLocalization { get; set; }

    bool? IData.RequireLocalization => RequireLocalization;

    public enum DataKey
    {
        Custom,
        ParentData
    }
}