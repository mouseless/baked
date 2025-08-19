using Baked.Ui;

using static Baked.Theme.Admin.Components;

namespace Baked.Theme.Admin;

public record Conditional
{
    public IComponentDescriptor Fallback { get; set; } = String();
    public List<Condition> Conditions { get; init; } = [];

    public record Condition(string Prop, object Value, IComponentDescriptor Component)
    {
        public string Prop { get; set; } = Prop;
        public object Value { get; set; } = Value;
        public IComponentDescriptor Component { get; set; } = Component;
    }
}