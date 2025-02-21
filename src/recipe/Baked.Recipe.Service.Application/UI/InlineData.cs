namespace Baked.UI;

public class InlineData : IData
{
    public string Type => "Inline";
    public object Value { get; set; } = default!;
}