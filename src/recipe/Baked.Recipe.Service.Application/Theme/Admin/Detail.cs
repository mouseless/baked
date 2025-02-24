using Baked.Ui;
using Humanizer;

namespace Baked.Theme.Admin;

public record Detail : IComponentSchema
{
    public required string Title { get; set; }
    public IComponentDescriptor? Header { get; set; }
    public List<Property> Props { get; set; } = [];

    public class Property(string key)
    {
        public string Key { get; } = key;
        public string Title { get; set; } = key.Humanize();
        public IComponentDescriptor Component { get; set; } = Components.String;
    }
}