using Do.Architecture;

namespace Do.Greeting;

public class GreetingConfigurator
{
    public IFeature Disabled() => Feature.Empty;
}
