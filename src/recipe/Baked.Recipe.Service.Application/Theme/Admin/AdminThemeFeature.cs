using Baked.Architecture;

namespace Baked.Theme.Admin;

public class AdminThemeFeature(List<string>? _componentExports) : IFeature<ThemeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureComponentExports(exports =>
        {
            exports.AddRange([.. _componentExports ?? []]);
            exports.AddFromExtensions(typeof(Components));
        });
    }
}