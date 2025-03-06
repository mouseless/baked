namespace Baked.Ui;

public record InlineData(object Value)
    : IData
{
    public string Type => "Inline";
    public object Value { get; set; } = Value;
}