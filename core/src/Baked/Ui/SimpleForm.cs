namespace Baked.Ui;

public record SimpleForm : IComponentSchema
{
    public string? Label { get; set; }
    public List<Input> Inputs { get; init; } = [];
}