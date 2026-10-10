namespace Baked.Ui;

public record Textarea : IComponentSchema, ILabeler
{
    public Labeler? Label { get; set; }
}