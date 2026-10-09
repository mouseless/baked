namespace Baked.Ui;

public record InputDate : IComponentSchema, ILabeler
{
    public Labeler? Label { get; set; }
    public string? Format { get; set; }
    public bool? UsePicker { get; set; }
}