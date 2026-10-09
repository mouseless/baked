namespace Baked.Ui;

public record InputUrl : IComponentSchema, ILabeler
{
    public Labeler? Label { get; set; }
}