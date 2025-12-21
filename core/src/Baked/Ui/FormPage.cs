namespace Baked.Ui;

public record FormPage(string Path, PageTitle Title, Button Button)
    : PageSchemaBase(Path)
{
    public PageTitle Title { get; set; } = Title;
    public Button Button { get; set; } = Button;
    public List<Input> Inputs { get; init; } = [];
}