namespace Baked.Ui;

public record Header : IComponentSchema
{
    public Dictionary<string, Item> Sitemap { get; init; } = [];

    public record Item
    {
        public string Route { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? Title { get; set; }
        public string? ParentRoute { get; set; }
    }
}