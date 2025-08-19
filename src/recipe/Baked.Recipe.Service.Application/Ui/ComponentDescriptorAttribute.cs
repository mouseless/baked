namespace Baked.Ui;

public class ComponentDescriptorAttribute<TSchema>(TSchema schema)
    : Attribute, IComponentDescriptor where TSchema : IComponentSchema
{
    public string Type => typeof(TSchema).Name;
    public TSchema Schema { get; set; } = schema;
    public IData? Data { get; set; }

    string IComponentDescriptor.Type => Type;
    IComponentSchema IComponentDescriptor.Schema => Schema;
}