using Baked.Ui;

namespace Baked.Theme.Admin;

public record QueryParameter(string Name, IComponentDescriptor Component)
{
    public string Name { get; set; } = Name;
    public IComponentDescriptor Component { get; set; } = Component;
}