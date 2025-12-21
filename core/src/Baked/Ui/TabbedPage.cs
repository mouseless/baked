namespace Baked.Ui;

public record TabbedPage(string Path, PageTitle Title)
    : PageSchemaBase(Path)
{
    public PageTitle Title { get; set; } = Title;
    public List<Input> Inputs { get; init; } = [];
    public List<Tab> Tabs { get; init; } = [];
}