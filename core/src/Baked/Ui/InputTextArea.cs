namespace Baked.Ui;

public record InputTextarea : IComponentSchema, ILabeler
{
    public Label? Label { get; set; }
}