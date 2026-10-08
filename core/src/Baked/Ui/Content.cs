using B = Baked.Ui.Components;

namespace Baked.Ui;

public record Content(string Key)
    : IOrderableSchema
{
    public IComponentDescriptor Component { get; set; } = B.MissingComponent();
    public string Key { get; set; } = Key;
    public bool? Narrow { get; set; }
    public bool? Side { get; set; }
}