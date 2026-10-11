namespace Baked.Ui;

public record SideMenu : IComponentSchema
{
    public string? Logo { get; set; }
    public string? LargeLogo { get; set; }
    public List<Item> Menu { get; init; } = [];
    public IComponentDescriptor? Footer { get; set; }

    public record Item
    {
        public string Route { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string? Title { get; set; }
        public bool? Disabled { get; set; }
    }
}