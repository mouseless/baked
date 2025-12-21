namespace Baked.Ui;

public record SimpleForm(string Title, Button SubmitButton) : IComponentSchema
{
    public string Title { get; set; } = Title;
    public Button SubmitButton { get; set; } = SubmitButton;
    public List<Input> Inputs { get; init; } = [];
    public Dialog? DialogTemplate { get; set; }

    public record Dialog(Button ToggleButton, Button CancelButton);
}