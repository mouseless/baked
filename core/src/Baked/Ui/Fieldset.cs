namespace Baked.Ui;

public record Fieldset(string TitleProp)
    : IComponentSchema
{
    public string TitleProp { get; set; } = TitleProp;
    public List<Field> Fields { get; init; } = [];
}