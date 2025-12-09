namespace Baked.Ui;

public record ReportPage(string Path, PageTitle Title)
    : PageSchemaBase(Path)
{
    public PageTitle Title { get; set; } = Title;
    public List<Input> Inputs { get; init; } = [];
    public List<Tab> Tabs { get; init; } = [];

    public record Tab(string Id)
        : ISupportsReaction
    {
        public string Id { get; set; } = Id;
        public string? Title { get; set; }
        public List<Content> Contents { get; init; } = [];
        public bool? FullScreen { get; set; }
        public IComponentDescriptor? Icon { get; set; }
        public Dictionary<string, Reaction>? On { get; set; }

        public record Content(IComponentDescriptor Component, string Key)
            : ISupportsReaction
        {
            public IComponentDescriptor Component { get; set; } = Component;
            public string Key { get; set; } = Key;
            public bool? Narrow { get; set; }
            public Dictionary<string, Reaction>? On { get; set; }
        }
    }
}