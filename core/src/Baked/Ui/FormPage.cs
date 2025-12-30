namespace Baked.Ui;

public record FormPage(string Path, PageTitle Title, Button Submit)
    : PageSchemaBase(Path)
{
    public PageTitle Title { get; set; } = Title;
    public Button Submit { get; set; } = Submit;
    public List<Input> Inputs { get; init; } = [];
}