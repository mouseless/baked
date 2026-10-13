namespace Baked.Ui;

public record SimpleForm : IComponentSchema
{
    public string? Title { get; set; }
    public Button Submit { get; set; } = new();
    public List<Input> Inputs { get; init; } = [];
    public bool? Horizontal { get; set; }
    public List<ValidationComposable>? Validations { get; set; }
    public bool? ShowValidationSummary { get; set; }
}