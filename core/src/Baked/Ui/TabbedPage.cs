namespace Baked.Ui;

public record TabbedPage : PageSchemaBase
{
    public IComponentDescriptor Title { get; set; } = MissingComponent.Empty;
    public List<Input> Inputs { get; init; } = [];
    public List<Tab> Tabs { get; init; } = [];
}