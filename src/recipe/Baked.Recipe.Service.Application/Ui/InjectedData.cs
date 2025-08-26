namespace Baked.Ui;

public record InjectedData : IData
{
    public string Type => "Injected";
    public DataKey Key { get; set; } = DataKey.Custom;
    public string? Prop { get; set; }
    internal bool? RequireLocalization { get; set; }

    bool? IData.RequireLocalization => RequireLocalization;

    public enum DataKey
    {
        Custom,
        ParentData
    }
}