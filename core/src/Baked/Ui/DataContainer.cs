using B = Baked.Ui.Components;

namespace Baked.Ui;

public record DataContainer : IComponentSchema
{
    public List<Input> Inputs { get; init; } = [];
    public IComponentDescriptor Content { get; set; } = B.MissingComponent();
    public List<IComponentDescriptor>? Actions { get; set; }
}