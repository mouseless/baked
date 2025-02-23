using Baked.Ui;

namespace Baked.Theme.Admin;

public record DetailSchema : IComponentSchema
{
    public required string Title { get; set; }
    public IComponentDescriptor? Header { get; set; }
    public List<Property> Props { get; set; } = [];

    public record Property
    {
        public required string Key { get; set; }
        public required string Title { get; set; }
        public required IComponentDescriptor Component { get; set; }
    }
}