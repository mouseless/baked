using Baked.Ui;

namespace Baked.Test.Ui;

public record ContainerPage(string Path) : PageSchemaBase(Path)
{
    public List<IComponentDescriptor> Contents { get; init; } = [];
}