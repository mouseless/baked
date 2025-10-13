using Baked.Architecture;

namespace Baked.Theme;

public class ThemeConfigurator
{
    public IFeature<ThemeConfigurator> Disabled() =>
        Feature.Empty<ThemeConfigurator>();
}