namespace Baked.Ui;

public record InputCheckbox : IComponentSchema, ILabeler
{
    public Label? Label { get; set; }
    public bool? Indeterminate { get; set; }
}