namespace Baked.Ui;

public record CompositeData()
    : IData
{
    public string Type => "Composite";
    public List<IData> Parts { get; init; } = [];

    bool? _requireLocalization;
    bool? IData.RequireLocalization => _requireLocalization;

    public void SetRequireLocalization(bool? requireLocalization) =>
        _requireLocalization = requireLocalization;
}