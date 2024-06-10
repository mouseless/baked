using Baked.Architecture;

namespace Baked.Greeting;

public class GreetingConfigurator
{
    public IFeature<GreetingConfigurator> Disabled() =>
        Feature.Empty<GreetingConfigurator>();
}