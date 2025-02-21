namespace Baked.UI;

public class ComponentDescriptor(string type) : IComponentDescriptor
{
    public string Type { get; set; } = type;
    public IData? Data { get; set; }

    IComponentSchema? IComponentDescriptor.Schema => default!;
}

public class ComponentDescriptor<TSchema>(TSchema schema) : IComponentDescriptor
    where TSchema : IComponentSchema
{
    public string Type => typeof(TSchema).Name.Replace("Schema", string.Empty);
    public TSchema Schema { get; set; } = schema;
    public IData? Data { get; set; }

    string IComponentDescriptor.Type => Type;
    IComponentSchema? IComponentDescriptor.Schema => Schema;
}