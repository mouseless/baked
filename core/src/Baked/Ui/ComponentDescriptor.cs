namespace Baked.Ui;

public class ComponentDescriptor<TSchema>(TSchema schema)
    : IComponentDescriptor where TSchema : IComponentSchema
{
    public string Type { get; set; } = typeof(TSchema).Name;
    public TSchema Schema { get; set; } = schema;
    public IData? Data { get; set; }
    public IAction? Action { get; set; }
    public Dictionary<string, ITrigger>? Reactions { get; set; }

    public void Override<TNewSchema>(ComponentDescriptor<TNewSchema> component)
        where TNewSchema : TSchema, IComponentOverride<TSchema>
    {
        Type = typeof(TNewSchema).Name;
        component.Schema.Base = Schema;
        Schema = component.Schema;
    }

    IComponentSchema IComponentDescriptor.Schema => Schema;
}