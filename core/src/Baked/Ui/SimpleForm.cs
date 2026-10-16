namespace Baked.Ui;

public record SimpleForm : IComponentSchema
{
    public string? Title { get; set; }
    public IComponentDescriptor? Message { get; set; }
    public List<Input> Inputs { get; init; } = [];
    public Button Submit { get; set; } = new();
    public List<ValidationComposable>? Validations { get; set; }
    public bool? Horizontal { get; set; }
    public bool? ShowValidationSummary { get; set; }
}