using Baked.Architecture;

namespace Baked.Theme.Admin;

public class AdminThemeFeature(IEnumerable<string> _componentExports)
    : IFeature<ThemeConfigurator>
{
    public AdminThemeFeature() : this([]) { }

    public virtual void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureComponentExports(exports =>
        {
            exports.AddRange(_componentExports);
            exports.AddFromExtensions(typeof(Components));
        });
    }
}