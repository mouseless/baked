namespace Baked.Ui;

public record SimpleForm : IComponentSchema
{
    public string? ButtonIcon { get; set; }
    public string? ButtonLabel { get; set; }
    public string? ButtonVariant { get; set; }
    public bool? ButtonRounded { get; set; }
    public List<Input> Inputs { get; init; } = [];
    public bool? Dialog { get; set; }
}