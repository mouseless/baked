namespace Baked.Ui;

public record Date : IComponentSchema
{
    public string? Prop { get; set; }
    public string? Format { get; set; }
}