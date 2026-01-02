using Baked.Ui;

namespace Baked.Playground.Ui;

public record Container : IComponentSchema
{
    public List<IComponentDescriptor> Contents { get; init; } = [];
}