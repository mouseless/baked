using Baked.Ui;

namespace Baked.Theme.Default;

public record MenuPage(string Path)
    : PageSchemaBase(Path)
{
    public string? FilterPageContextKey { get; set; }
    public IComponentDescriptor? Header { get; set; }
    public List<Section> Sections { get; init; } = [];

    public record Section
    {
        public string? Title { get; set; }
        public List<Filterable> Links { get; init; } = [];
    }
}