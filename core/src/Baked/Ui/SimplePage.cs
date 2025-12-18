namespace Baked.Ui;

public record SimplePage(string Path, IComponentDescriptor Title)
    : PageSchemaBase(Path)
{
    public IComponentDescriptor Title { get; set; } = Title;
    public List<Content> Contents { get; init; } = [];

    public record Content(IComponentDescriptor Component, string Key)
    {
        public IComponentDescriptor Component { get; set; } = Component;
        public string Key { get; set; } = Key;
    }
}