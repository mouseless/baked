namespace Baked.Ui;

public class InlineData : IData
{
    public string Type => "Inline";
    public required object Value { get; set; }
}