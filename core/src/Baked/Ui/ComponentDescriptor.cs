namespace Baked.Ui;

public class ComponentDescriptor<TSchema>(TSchema schema)
    : IComponentDescriptor where TSchema : IComponentSchema
{
    public string Type => typeof(TSchema).Name;
    public TSchema Schema { get; set; } = schema;
    public IData? Data { get; set; }
    public IAction? Action { get; set; }
    public IAction? PostAction { get; set; }
    // TODO - review this in form components
    public Reaction? Reaction { get; set; }

    string IComponentDescriptor.Type => Type;
    IComponentSchema IComponentDescriptor.Schema => Schema;
}