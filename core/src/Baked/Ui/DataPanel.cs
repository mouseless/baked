using B = Baked.Ui.Components;

namespace Baked.Ui;

public record DataPanel : IComponentSchema
{
    public IData Title { get; set; } = Datas.Inline(nameof(MissingComponent));
    public bool? Collapsed { get; set; }
    public bool? LocalizeTitle { get; set; }
    public List<Input> Inputs { get; init; } = [];
    public IComponentDescriptor Content { get; set; } = B.MissingComponent();
    public bool? Toggleable { get; set; }
}