namespace Baked.Ui;

public record InputMoney : IComponentSchema, ILabeler
{
    public Label? Label { get; set; }
    public string? Icon { get; set; }
}