using B = Baked.Ui.Components;

namespace Baked.Ui;

public record Content : IOrderableSchema
{
    public IComponentDescriptor Component { get; set; } = B.MissingComponent();
    public string Key { get; set; } = string.Empty;
    public bool? Narrow { get; set; }
    public bool? Side { get; set; }
}