namespace Baked.Ui;

public record ComputedData(string Composable)
    : IData
{
    public string Type => "Computed";
    public string Composable { get; set; } = Composable;
    public List<object> Args { get; init; } = [];
    internal bool? RequireLocalization { get; set; }

    bool? IData.RequireLocalization => RequireLocalization;
}