namespace Baked.Ui;

public record ReportPage(string Path, PageTitle Title)
    : PageSchemaBase(Path)
{
    public PageTitle Title { get; set; } = Title;
    public List<Input> Inputs { get; init; } = [];
    public List<Tab> Tabs { get; init; } = [];
}