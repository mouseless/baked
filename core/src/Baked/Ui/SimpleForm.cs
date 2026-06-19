namespace Baked.Ui;

public record SimpleForm(string Title, Button Submit) : IComponentSchema
{
    public string Title { get; set; } = Title;
    public Button Submit { get; set; } = Submit;
    public List<Input> Inputs { get; init; } = [];
    public bool? Horizontal { get; set; }
    public Dialog? DialogOptions { get; set; }
    public List<ValidationComposable>? Validations { get; set; }
    public bool? ShowValidationSummary { get; set; }
    public bool? AlwaysShowTitle { get; set; }

    public record Dialog(Button Open, Button Cancel)
    {
        public Button Open { get; set; } = Open;
        public Button Cancel { get; set; } = Cancel;
        public string? Message { get; set; }
    }
}