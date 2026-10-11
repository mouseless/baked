namespace Baked.Ui;

public record DataContainer : IComponentSchema
{
    public List<Input> Inputs { get; init; } = [];
    public IComponentDescriptor Content { get; set; } = MissingComponent.Empty;
    public List<IComponentDescriptor>? Actions { get; set; }
}