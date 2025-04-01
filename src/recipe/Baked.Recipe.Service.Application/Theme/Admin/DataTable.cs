using Baked.Ui;

namespace Baked.Theme.Admin;

public record DataTable : IComponentSchema
{
    public List<Column> Columns { get; init; } = [];
    public string? DataKey { get; set; }
    public bool Paginator { get; set; }
    public int? Rows { get; set; }
    public int? RowsWhenLoading { get; set; }

    public record Column(string Prop, string Title, IComponentDescriptor Component)
    {
        public string Prop { get; set; } = Prop;
        public string Title { get; set; } = Title;
        public IComponentDescriptor Component { get; set; } = Component;
        public bool MinWidth { get; set; }
    }
}