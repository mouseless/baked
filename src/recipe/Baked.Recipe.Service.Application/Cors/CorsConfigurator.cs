using Baked.Architecture;

namespace Baked.Cors;

public class CorsConfigurator
{
    public IFeature<CorsConfigurator> Disabled() =>
        Feature.Empty<CorsConfigurator>();
}