namespace Baked.Ui;

public record DataPanel(IData Title, IComponentDescriptor Content)
    : IComponentSchema
{
    public IData Title { get; set; } = Title;
    public bool? Collapsed { get; set; }
    public bool? LocalizeTitle { get; set; } = Title.RequireLocalization;
    public List<Parameter> Parameters { get; init; } = [];
    public IComponentDescriptor Content { get; set; } = Content;
}