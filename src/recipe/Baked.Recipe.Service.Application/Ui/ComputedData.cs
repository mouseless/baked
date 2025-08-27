namespace Baked.Ui;

public record ComputedData(string Composable)
    : IData
{
    public string Type => "Computed";
    public string Composable { get; set; } = Composable;
    public List<object> Args { get; init; } = [];

    bool? _requireLocalization;
    bool? IData.RequireLocalization => _requireLocalization;

    public void SetRequireLocalization(bool? requireLocalization) =>
        _requireLocalization = requireLocalization;
}