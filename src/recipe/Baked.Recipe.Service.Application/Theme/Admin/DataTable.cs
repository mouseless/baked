using Baked.Ui;

namespace Baked.Theme.Admin;

public record DataTable : IComponentSchema
{
    public List<Column> Columns { get; init; } = [];
    public int? RowCountWhenLoading { get; set; }

    public record Column(string Prop, string Title, IComponentDescriptor Component)
    {
        public string Prop { get; set; } = Prop;
        public string Title { get; set; } = Title;
        public IComponentDescriptor Component { get; set; } = Component;
        public bool MinWidth { get; set; }
    }
}