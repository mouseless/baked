namespace Baked.Ui;

public record Tab(string Id)
    : ISupportsReaction
{
    public string Id { get; set; } = Id;
    public string? Title { get; set; }
    public List<Content> Contents { get; init; } = [];
    public bool? FullScreen { get; set; }
    public IComponentDescriptor? Icon { get; set; }
    public Dictionary<string, ITrigger>? Reactions { get; set; }

    public record Content(IComponentDescriptor Component, string Key)
    {
        public IComponentDescriptor Component { get; set; } = Component;
        public string Key { get; set; } = Key;
        public bool? Narrow { get; set; }
    }
}