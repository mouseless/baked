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
    public Export? ExportOptions { get; set; }

    public record Column(string Prop, Conditional Component, bool Exportable)
    {
        public string Prop { get; set; } = Prop;
        public Conditional Component { get; set; } = Component;
        public string? Title { get; set; }
        public bool MinWidth { get; set; }
        public bool Exportable { get; set; } = Exportable;
        public string? ExportHeader { get; set; }
    }

    public record Footer(string Label)
    {
        public string Label { get; set; } = Label;
        public List<Column> Columns { get; init; } = [];
    }

    public record Export(string CsvSeperator, string FileName)
    {
        public string CsvSeperator { get; set; } = CsvSeperator;
        public string? Formatter { get; set; }
        public string FileName { get; set; } = FileName;
        public string? Label { get; set; }
    }
}