namespace Baked.Ui;

public record DataContainer(IComponentDescriptor Content)
    : IComponentSchema
{
    public List<IComponentDescriptor> Actions { get; init; } = [];
    public List<Input> Inputs { get; init; } = [];
    public IComponentDescriptor Content { get; set; } = Content;
}