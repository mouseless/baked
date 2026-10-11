namespace Baked.Ui;

public record InputMailAddress : IComponentSchema, ILabeler
{
    public Labeler? Label { get; set; }
}