namespace Baked.Ui;

public class ComponentDescriptor<TSchema>(TSchema schema)
    : IComponentDescriptor where TSchema : IComponentSchema
{
    public string Type => typeof(TSchema).Name;
    public TSchema Schema { get; set; } = schema;
    public IData? Data { get; set; }
    // TODO - review this in form components
    public string? Binding { get; set; }

    string IComponentDescriptor.Type => Type;
    IComponentSchema IComponentDescriptor.Schema => Schema;
}