namespace Baked.Ui;

public record InputTextArea : IComponentSchema, ILabeler
{
    public Label? Label { get; set; }
}