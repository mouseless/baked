namespace Baked.Ui;

public record SimplePage(string Path, IComponentDescriptor Title)
    : PageSchemaBase(Path)
{
    public IComponentDescriptor Title { get; set; } = Title;
    public List<Content> Contents { get; init; } = [];
}