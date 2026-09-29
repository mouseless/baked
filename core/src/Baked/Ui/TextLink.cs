namespace Baked.Ui;

public record TextLink : IComponentSchema
{
    public string? Icon { get; set; }
    public int? MaxLength { get; set; }
}