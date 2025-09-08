using Baked.Theme;

namespace Baked.Ui;

public interface IComponentDescriptorBuilder
{
    bool AppliesTo(ComponentContext context);
    IComponentDescriptor Build(ComponentContext context);
}