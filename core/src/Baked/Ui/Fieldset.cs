namespace Baked.Ui;

public record Fieldset : IComponentSchema
{
    public string TitleProp { get; set; } = string.Empty;
    public List<Field> Fields { get; init; } = [];
}