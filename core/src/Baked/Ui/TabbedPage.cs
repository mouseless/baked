namespace Baked.Ui;

public record TabbedPage(string Path, IComponentDescriptor Title)
    : PageSchemaBase(Path)
{
    public IComponentDescriptor Title { get; set; } = Title;
    public List<Input> Inputs { get; init; } = [];
    public List<Tab> Tabs { get; init; } = [];
}