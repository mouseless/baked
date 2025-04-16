using Baked.Ui;

namespace Baked.Theme.Admin;

public record Conditional(string Prop, object Value, IComponentDescriptor Component)
{
    public string Prop { get; set; } = Prop;
    public object Value { get; set; } = Value;
    public IComponentDescriptor Component { get; set; } = Component;
}