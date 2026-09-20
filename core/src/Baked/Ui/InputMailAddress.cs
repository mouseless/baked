namespace Baked.Ui;

public record InputMailAddress : IComponentSchema, ILabeler
{
    public Label? Label { get; set; }
}