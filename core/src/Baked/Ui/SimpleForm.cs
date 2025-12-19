namespace Baked.Ui;

public record SimpleForm(string Name, Button SubmitButton) : IComponentSchema
{
    public string Name { get; set; } = Name;
    public Button SubmitButton { get; set; } = SubmitButton;
    public List<Input> Inputs { get; init; } = [];
    public Dialog? DialogTemplate { get; set; }

    public record Dialog(Button ToggleButton, Button CancelButton);
}