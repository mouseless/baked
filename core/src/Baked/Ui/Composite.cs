namespace Baked.Ui;

public record Composite : IComponentSchema
{
    public List<IComponentDescriptor> Parts { get; init; } = [];
}