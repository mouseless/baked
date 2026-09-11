namespace Baked.Ui;

public record InputUrl : IComponentSchema, ILabeler
{
    public Label? Label { get; set; }
}