namespace Baked.Ui;

public record InputText : IComponentSchema, ILabeler
{
    public Labeler? Label { get; set; }
    public string? TargetProp { get; set; }
}