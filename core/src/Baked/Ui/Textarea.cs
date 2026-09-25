namespace Baked.Ui;

public record Textarea : IComponentSchema, ILabeler
{
    public Label? Label { get; set; }
}