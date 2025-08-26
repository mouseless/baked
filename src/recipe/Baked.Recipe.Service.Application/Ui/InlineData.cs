namespace Baked.Ui;

public record InlineData(object Value)
    : IData
{
    public string Type => "Inline";
    public object Value { get; set; } = Value;

    bool? _requireLocalization = true;
    bool? IData.RequireLocalization => _requireLocalization;

    public void SetRequireLocalization(bool? requireLocalization) =>
        _requireLocalization = requireLocalization;
}