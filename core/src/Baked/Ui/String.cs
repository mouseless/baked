namespace Baked.Ui;

public record String : IComponentSchema
{
    public int? MaxLength { get; set; }
}