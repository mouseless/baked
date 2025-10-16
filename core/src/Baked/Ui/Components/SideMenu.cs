namespace Baked.Ui.Component;

public record SideMenu : IComponentSchema
{
    public string Logo { get; set; } = "logo.svg";
    public string LargeLogo { get; set; } = "logo-full.svg";
    public List<Item> Menu { get; init; } = [];
    public IComponentDescriptor? Footer { get; set; }

    public record Item(string Route, string Icon)
    {
        public string Route { get; set; } = Route;
        public string Icon { get; set; } = Icon;
        public string? Title { get; set; }
        public bool? Disabled { get; set; }
    }
}