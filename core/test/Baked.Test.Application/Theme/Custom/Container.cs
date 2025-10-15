using Baked.Ui;

namespace Baked.Test.Theme.Custom;

public record Container : IComponentSchema
{
    public List<IComponentDescriptor> Contents { get; init; } = [];
}