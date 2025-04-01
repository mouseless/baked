using Baked.Ui;

namespace Baked.Theme.Admin;

public record DataPanel(IData Title, IComponentDescriptor Content)
    : IComponentSchema
{
    public IData Title { get; set; } = Title;
    public bool Collapsed { get; set; }
    public List<Parameter> Parameters { get; init; } = [];
    public IComponentDescriptor Content { get; set; } = Content;
}