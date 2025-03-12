using Baked.Ui;

namespace Baked.Theme.Admin;

public record ApiPanel(string Title, IComponentDescriptor Viewer)
    : IComponentSchema
{
    public string Title { get; set; } = Title;
    public bool Collapsed { get; set; }
    public IComponentDescriptor Viewer { get; set; } = Viewer;
}