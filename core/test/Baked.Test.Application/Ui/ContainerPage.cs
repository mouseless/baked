using Baked.Ui;

namespace Baked.Test.Ui;

// TODO - review this in form components
// temporary page template with simple flex container
public record ContainerPage(string Path) : PageSchemaBase(Path)
{
    public List<IComponentDescriptor> Contents { get; init; } = [];
}