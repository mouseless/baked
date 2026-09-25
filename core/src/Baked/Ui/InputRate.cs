namespace Baked.Ui;

public record InputRate : ILabeler, IComponentSchema
{
    public Label? Label { get; set; }
    public bool? Disabled { get; set; }
    public int? Max { get; set; }
}