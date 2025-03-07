namespace Baked.Ui;

public class ComponentDescriptorAttribute<TSchema>(TSchema schema)
    : Attribute, IComponentDescriptor where TSchema : IComponentSchema
{
    public string Type => typeof(TSchema).Name;
    public string? Key { get; set; }
    public TSchema Schema { get; } = schema;
    public string? Name { get; set; }
    public IData? Data { get; set; }

    string IComponentDescriptor.Type => Type;
    IComponentSchema? IComponentDescriptor.Schema => Schema;
}