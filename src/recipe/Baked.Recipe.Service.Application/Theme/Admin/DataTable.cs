using Baked.Ui;

namespace Baked.Theme.Admin;

public record DataTable : IComponentSchema
{
    public List<Column> Columns { get; init; } = [];
    public string? DataKey { get; set; }
    public string? ItemsProp { get; set; }
    public bool Paginator { get; set; }
    public int? Rows { get; set; }
    public int? RowsWhenLoading { get; set; }
    public string? ScrollHeight { get; set; }
    public Footer? FooterTemplate { get; set; }

    public record Column(string Prop, Conditional Component)
    {
        public string Prop { get; set; } = Prop;
        public Conditional Component { get; set; } = Component;
        public string? Title { get; set; }
        public bool MinWidth { get; set; }
    }

    public record Footer(string Label)
    {
        public string Label { get; set; } = Label;
        public List<Column> Columns { get; init; } = [];
    }
}