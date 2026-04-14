namespace Baked.Ui;

public record Text : IComponentSchema
{
    public int? MaxLength { get; set; }
    public string? Prop { get; set; }
}