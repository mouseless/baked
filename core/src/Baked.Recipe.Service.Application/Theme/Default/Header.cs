using Baked.Ui;

namespace Baked.Theme.Default;

public record Header : IComponentSchema
{
    public Dictionary<string, Item> Sitemap { get; init; } = [];

    public record Item(string Route)
    {
        public string Route { get; set; } = Route;
        public string? Icon { get; set; }
        public string? Title { get; set; }
        public string? ParentRoute { get; set; }
    }
}