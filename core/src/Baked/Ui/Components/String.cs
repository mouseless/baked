namespace Baked.Ui.Component;

public record String : IComponentSchema
{
    public int? MaxLength { get; set; }
}