using Baked.Ui;

namespace Baked.Theme.Admin;

public record ErrorPage : IComponentSchema
{
    public Dictionary<int, object> ErrorInfos { get; init; } = [];
    public List<IComponentDescriptor> SafeLinks { get; init; } = [];
}