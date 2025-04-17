using Baked.Ui;

namespace Baked.Theme.Admin;

public record Conditional(IComponentDescriptor Fallback)
{
    public IComponentDescriptor Fallback { get; set; } = Fallback;
    public List<Condition> Conditions { get; init; } = [];

    public record Condition(string Prop, object Value, IComponentDescriptor Component)
    {
        public string Prop { get; set; } = Prop;
        public object Value { get; set; } = Value;
        public IComponentDescriptor Component { get; set; } = Component;
    }
}