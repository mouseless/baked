namespace Baked.Ui;

public record SimpleForm(string Name, Button SubmitButton) : IComponentSchema
{
    public string Name { get; set; } = Name;
    public Button SubmitButton { get; set; } = SubmitButton;
    public List<Input> Inputs { get; init; } = [];
    public bool? Dialog { get; set; }
    public Button? DialogToggleButton { get; set; }
    public Button? DialogCancelButton { get; set; }
}