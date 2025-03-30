namespace Baked.Ui;

public class ComponentDescriptor(string type,
    IComponentSchema? schema = default
) : IComponentDescriptor
{
    public string Type { get; set; } = type;
    public string? Key { get; set; }
    public IComponentSchema? Schema { get; } = schema;
    public IData? Data { get; set; }
}