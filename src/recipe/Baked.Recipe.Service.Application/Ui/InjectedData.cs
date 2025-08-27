namespace Baked.Ui;

public record InjectedData : IData
{
    public string Type => "Injected";
    public DataKey Key { get; set; } = DataKey.Custom;
    public string? Prop { get; set; }

    bool? _requireLocalization;
    bool? IData.RequireLocalization => _requireLocalization;

    public void SetRequireLocalization(bool? requireLocalization) =>
        _requireLocalization = requireLocalization;

    public enum DataKey
    {
        Custom,
        ParentData
    }
}