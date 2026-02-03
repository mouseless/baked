namespace Baked.Ui;

public record DataPanel(IData Title, IComponentDescriptor Content)
    : IComponentSchema
{
    public IData Title { get; set; } = Title;
    public bool? Collapsed { get; set; }
    public bool? LocalizeTitle { get; set; } = Title.RequireLocalization;
    public List<Input> Inputs { get; init; } = [];
    public IComponentDescriptor Content { get; set; } = Content;
    public bool? Toggleable { get; set; }
}