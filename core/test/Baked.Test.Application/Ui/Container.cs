using Baked.Ui;

namespace Baked.Test.Ui;

public record Container : IComponentSchema
{
    public List<IComponentDescriptor> Contents { get; init; } = [];
}