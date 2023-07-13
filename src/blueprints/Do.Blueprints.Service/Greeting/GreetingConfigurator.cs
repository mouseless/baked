using Do.Architecture;

namespace Do.Blueprints.Service.Greeting;

public class GreetingConfigurator
{
    public IFeature Disabled() => Feature.Empty;
}
