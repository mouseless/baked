namespace Baked.Ui.Component;

public record ReportPage(string Path, PageTitle Title)
    : PageSchemaBase(Path)
{
    public PageTitle Title { get; set; } = Title;
    public List<Parameter> QueryParameters { get; init; } = [];
    public List<Tab> Tabs { get; init; } = [];

    public record Tab(string Id)
    {
        public string Id { get; set; } = Id;
        public string? Title { get; set; }
        public List<Content> Contents { get; init; } = [];
        public bool? FullScreen { get; set; }
        public IComponentDescriptor? Icon { get; set; }
        public string? ShowWhen { get; set; }

        public record Content(IComponentDescriptor Component, string Key)
        {
            public IComponentDescriptor Component { get; set; } = Component;
            public string Key { get; set; } = Key;
            public bool? Narrow { get; set; }
            public string? ShowWhen { get; set; }
        }
    }
}