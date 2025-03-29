using Baked.Ui;

namespace Baked.Theme.Admin;

public record ReportPage(string Name, PageTitle Title) :
    INamedComponentSchema
{
    public string Name { get; set; } = Name;
    public PageTitle Title { get; set; } = Title;
    public List<Parameter> QueryParameters { get; init; } = [];
    public List<Tab> Tabs { get; init; } = [];

    public record Tab(string Id, string Title)
    {
        public string Id { get; set; } = Id;
        public string Title { get; set; } = Title;
        public IComponentDescriptor? Icon { get; set; }
        public List<Content> Contents { get; init; } = [];

        public record Content(IComponentDescriptor Component)
        {
            public IComponentDescriptor Component { get; set; } = Component;
            public bool FullScreen { get; set; }
            public bool Narrow { get; set; }
        }
    }
}