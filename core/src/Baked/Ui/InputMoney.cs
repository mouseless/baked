namespace Baked.Ui;

public record InputMoney : IComponentSchema, ILabeler
{
    public Labeler? Label { get; set; }
    public string? Icon { get; set; }
}