namespace Baked.Ui;

public record InputNumber : ILabeler, IComponentSchema
{
    public Label? Label { get; set; }
    public bool? NoGrouping { get; set; }
}