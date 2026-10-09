namespace Baked.Ui;

public record InputCheckbox : IComponentSchema, ILabeler
{
    public Labeler? Label { get; set; }
    public bool? Indeterminate { get; set; }
}