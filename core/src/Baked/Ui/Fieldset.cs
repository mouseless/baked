using B = Baked.Ui.Components;

namespace Baked.Ui;

public record Fieldset : IComponentSchema
{
    public string? TitleProp { get; set; }
    public List<Field> Fields { get; init; } = [];

    public record Field(string Name)
    {
        public string Name { get; set; } = Name;
        public IComponentDescriptor Component { get; set; } = B.Text();
    }
}