using Baked.Ui;

namespace Baked.Theme.Admin;

public record ErrorPage : IComponentSchema
{
    public List<IComponentDescriptor> SafeLinks { get; init; } = [];
}