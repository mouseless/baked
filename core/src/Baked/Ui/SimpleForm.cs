namespace Baked.Ui;

public record SimpleForm : IComponentSchema
{
    public string? ButtonIcon { get; set; }
    public string? ButtonLabel { get; set; }
    public List<Input> Inputs { get; init; } = [];
}