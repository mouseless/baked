namespace Baked.Ui;

public record SimpleForm : IComponentSchema
{
    public string Title { get; set; } = string.Empty;
    public Button Submit { get; set; } = new();
    public List<Input> Inputs { get; init; } = [];
    public bool? Horizontal { get; set; }
    public Dialog? DialogOptions { get; set; }
    public List<ValidationComposable>? Validations { get; set; }
    public bool? ShowValidationSummary { get; set; }
    public bool? AlwaysShowTitle { get; set; }

    public record Dialog
    {
        public Button Open { get; set; } = new();
        public Button Cancel { get; set; } = new();
        public string? Message { get; set; }
    }
}