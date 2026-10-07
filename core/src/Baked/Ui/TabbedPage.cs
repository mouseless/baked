using B = Baked.Ui.Components;

namespace Baked.Ui;

public record TabbedPage(string Path)
    : PageSchemaBase(Path)
{
    public IComponentDescriptor Title { get; set; } = B.MissingComponent();
    public List<Input> Inputs { get; init; } = [];
    public List<Tab> Tabs { get; init; } = [];
}