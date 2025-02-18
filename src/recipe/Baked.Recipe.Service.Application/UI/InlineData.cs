namespace Baked.UI;

public class InlineData : IData
{
    public string Type => "Inline";
    public dynamic Value { get; set; } = default!;
}