using Baked.Ui;

namespace Baked.Theme.Admin;

public record MenuPage(string Path)
    : IGeneratedComponentSchema
{
    public string Path { get; set; } = Path;
    public string? FilterPageContextKey { get; set; } = default;
    public IComponentDescriptor? Header { get; set; }
    public List<Section> Sections { get; init; } = [];

    public record Section
    {
        public string? Title { get; set; }
        public List<Filterable> Links { get; init; } = [];
    }
}