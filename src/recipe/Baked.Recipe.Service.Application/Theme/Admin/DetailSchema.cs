using Baked.UI;

namespace Baked.Theme.Admin;

public record DetailSchema : IComponentSchema
{
    public string Title { get; set; } = default!;
    public IComponentDescriptor Header { get; set; } = default!;
    public List<Property> Props { get; set; } = default!;
    public List<ComponentDescriptor<TableSchema>> Tables { get; } = [];

    public record Property
    {
        public string Key { get; set; } = default!;
        public string Title { get; set; } = default!;
        public IComponentDescriptor Component { get; set; } = default!;
    }
}