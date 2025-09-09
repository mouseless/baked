namespace Baked.Ui;

public class ComponentDescriptor<TSchema>(TSchema schema)
    : IComponentDescriptor
      where TSchema : IComponentSchema
{
    public string Type => typeof(TSchema).Name;
    public TSchema Schema { get; set; } = schema;
    public IData? Data { get; set; }

    IComponentSchema IComponentDescriptor.Schema => Schema;
}