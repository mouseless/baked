namespace Baked.Ui;

public record InputText : IComponentSchema, ILabeler
{
    public Label? Label { get; set; }
    public string? TargetProp { get; set; }
}