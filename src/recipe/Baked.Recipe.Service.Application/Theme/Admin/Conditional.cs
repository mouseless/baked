using Baked.Ui;

namespace Baked.Theme.Admin;

public record Conditional
{
    public IComponentDescriptor Fallback { get; set; } = Components.String();
    public List<Condition> Conditions { get; init; } = [];

    public record Condition(string Prop, object Value, IComponentDescriptor Component)
    {
        public string Prop { get; set; } = Prop;
        public object Value { get; set; } = Value;
        public IComponentDescriptor Component { get; set; } = Component;
    }
}