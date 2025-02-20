using Baked.UI;

namespace Baked.Theme.Admin;

public record TableSchema : IComponentSchema
{
    public string Title { get; set; } = default!;
    public List<Columm> Columns { get; set; } = [];

    public record Columm
    {
        public string Field { get; set; } = default!;
        public string Header { get; set; } = default!;
    }
}