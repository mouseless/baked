using Baked.UI;

namespace Baked.Theme.Admin;

public class MenuSchema : IComponentSchema
{
    public List<MenuItem> Items { get; set; } = default!;

    public record MenuItem(string Label,
        string? Url = default,
        List<MenuItem>? Items = default
    );
}
