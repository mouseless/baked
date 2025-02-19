namespace Baked.UI;

public class ComponentDescriptors : Dictionary<string, IComponentDescriptor>
{
    public string? SchemaDir { get; set; }
}