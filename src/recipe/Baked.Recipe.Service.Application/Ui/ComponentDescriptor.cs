namespace Baked.Ui;

public class ComponentDescriptor(string type)
    : IComponentDescriptor
{
    public string Type { get; set; } = type;
    public IData? Data { get; set; }

    IComponentSchema? IComponentDescriptor.Schema => default;
}