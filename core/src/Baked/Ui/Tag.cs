namespace Baked.Ui;

public record Tag : IComponentSchema
{
    public string? Icon { get; set; }
    public string? LocalizationPrefix { get; set; }
    public bool? LocalizeText { get; set; }
}