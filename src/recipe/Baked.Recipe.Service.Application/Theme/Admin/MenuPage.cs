using Baked.Ui;

namespace Baked.Theme.Admin;

public record MenuPage(string Path)
    : IGeneratedComponentSchema
{
    public string Path { get; set; } = Path;
    public IComponentDescriptor? Header { get; set; }
    public List<Section> Sections { get; init; } = [];

    public record Section
    {
        public string? Title { get; set; }
        public List<Filterable> Links { get; init; } = [];

        public record Filterable(string PageContextKey, string Title, IComponentDescriptor Link)
            : IComponentSchema
        {

        }
    }
}

