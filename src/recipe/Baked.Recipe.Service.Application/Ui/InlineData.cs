namespace Baked.Ui;

public record InlineData(object Value)
    : IData
{
    public string Type => "Inline";
    public object Value { get; set; } = Value;
    internal bool? RequireLocalization { get; set; }

    bool? IData.RequireLocalization => RequireLocalization;
}