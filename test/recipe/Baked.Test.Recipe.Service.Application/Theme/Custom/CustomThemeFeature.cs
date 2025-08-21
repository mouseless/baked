using Baked.Architecture;

namespace Baked.Theme.Admin;

public class CustomThemeFeature(List<string> _componentExports)
    : AdminThemeFeature(_componentExports)
{
    public override void Configure(LayerConfigurator configurator)
    {
        base.Configure(configurator);
    }
}