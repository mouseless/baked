using Do.Architecture;

namespace Do.Greeting;

public class GreetingConfigurator
{
    public IFeature<GreetingConfigurator> Disabled() =>
        Feature.Empty<GreetingConfigurator>();
}