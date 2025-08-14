using Baked.Ui;

namespace Baked.Theme.Admin;

public record DataTable : IComponentSchema
{
    public List<Column> Columns { get; init; } = [];
    public string? DataKey { get; set; }
    public string? ItemsProp { get; set; }
    public bool? Paginator { get; set; }
    public int? Rows { get; set; }
    public int? RowsWhenLoading { get; set; }
    public string? ScrollHeight { get; set; }
    public VirtualScroller? VirtualScrollerOptions { get; set; }
    public Footer? FooterTemplate { get; set; }
    public Export? ExportOptions { get; set; }

    public record Column(string Prop, Conditional Component)
    {
        public string Prop { get; set; } = Prop;
        public Conditional Component { get; set; } = Component;
        public string? Title { get; set; }
        public bool? AlignRight { get; set; }
        public bool? MinWidth { get; set; }
        public bool? Exportable { get; set; }
        public bool? Frozen { get; set; }
    }

    public record Footer(string Label)
    {
        public string Label { get; set; } = Label;
        public List<Column> Columns { get; init; } = [];
    }

    public record Export(string CsvSeparator, string FileName)
    {
        public string CsvSeparator { get; set; } = CsvSeparator;
        public string FileName { get; set; } = FileName;
        public string? Formatter { get; set; }
        public string? ButtonIcon { get; set; }
        public string? ButtonLabel { get; set; }
        public bool? AppendParameters { get; set; }
        public bool? LocalizeParameters { get; set; }
        public string? ParameterSeparator { get; set; }
    }

    public record VirtualScroller(int ItemSize)
    {
        public int ItemSize { get; set; } = ItemSize;
        public int? NumToleratedItems { get; set; }
        public bool? AppendOnly { get; set; }
    }
}