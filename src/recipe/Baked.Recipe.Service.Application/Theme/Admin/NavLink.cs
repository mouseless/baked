using Baked.Ui;

namespace Baked.Theme.Admin;

public class NavLink(string Path, string IdProp, string TextProp)
    : IComponentSchema
{
    public string Path { get; set; } = Path;
    public string IdProp { get; set; } = IdProp;
    public string TextProp { get; set; } = TextProp;
}