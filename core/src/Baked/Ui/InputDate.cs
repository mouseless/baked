namespace Baked.Ui;

public record InputDate : IComponentSchema, ILabeler
{
    public Label? Label { get; set; }
    public string? Format { get; set; }
    public bool? UsePicker { get; set; }
}