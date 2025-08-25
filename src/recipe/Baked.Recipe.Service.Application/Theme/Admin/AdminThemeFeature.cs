using Baked.Architecture;

namespace Baked.Theme.Admin;

public class AdminThemeFeature : IFeature<ThemeConfigurator>
{
    public virtual void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureComponentExports(exports =>
        {
            exports.AddFromExtensions(typeof(Components));
        });
    }
}