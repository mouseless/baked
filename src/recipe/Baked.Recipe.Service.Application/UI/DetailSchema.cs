namespace Baked.UI;

public record DetailSchema : IComponentSchema
{
    public string Title { get; set; } = default!;
    public IComponentDescriptor Header { get; set; } = default!;
    public List<Property> Props { get; set; } = default!;

    public record Property
    {
        public string Key { get; set; } = default!;
        public string Title { get; set; } = default!;
        public IComponentDescriptor Component { get; set; } = default!;
    }
}