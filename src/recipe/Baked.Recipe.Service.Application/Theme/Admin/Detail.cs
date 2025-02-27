using Baked.Ui;
using Humanizer;

namespace Baked.Theme.Admin;

public record Detail(string Title)
    : IComponentSchema
{
    public string Title { get; set; } = Title;
    public IComponentDescriptor? Header { get; set; }
    public List<Property> Props { get; init; } = [];

    public record Property(string Key)
    {
        public string Key { get; } = Key;
        public string Title { get; set; } = Key.Humanize();
        public IComponentDescriptor Component { get; set; } = Components.String;
    }
}

public record SideMenu : IComponentSchema
{
    public string Logo { get; set; } = "logo.svg";
    public List<Item> Menu { get; init; } = [];

    public record Item(string Title, string Icon, string Route)
    {
        public string Title { get; set; } = Title;
        public string Icon { get; set; } = Icon;
        public string Route { get; set; } = Route;
        public bool Soon { get; set; } = false;
    }
}