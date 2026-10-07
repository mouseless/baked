using B = Baked.Ui.Components;

namespace Baked.Ui;

public record SimplePage(string Path)
    : PageSchemaBase(Path)
{
    public IComponentDescriptor Title { get; set; } = B.MissingComponent();
    public List<Content> Contents { get; init; } = [];
}