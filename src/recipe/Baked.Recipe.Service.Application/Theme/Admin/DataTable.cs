using Baked.Ui;

namespace Baked.Theme.Admin;

public record DataTable : IComponentSchema
{
    public List<Column> Columns { get; init; } = [];
    public string? DataKey { get; set; }
    public bool Paginator { get; set; }
    public int? Rows { get; set; }
    public int? RowsWhenLoading { get; set; }
    public string? ScrollHeight { get; set; }
    public FooterRow? Footer { get; set; }

    public record Column(string Prop, Conditional Component)
    {
        public string Prop { get; set; } = Prop;
        public Conditional Component { get; set; } = Component;
        public string? Title { get; set; }
        public bool MinWidth { get; set; }
    }

    public record FooterRow(string Label)
    {
        public string Label { get; set; } = Label;
        public List<FooterColumn> Columns { get; init; } = [];
    }

    public record FooterColumn(string Prop, Conditional Component)
    {
        public string Prop { get; set; } = Prop;
        public Conditional Component { get; set; } = Component;
    }
}