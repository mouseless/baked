using Do.Greeting.Empty;

namespace Do.Greeting;

public class GreetingConfigurator
{
    public IGreetingFeature Disabled() => new EmptyGreetingFeature();
}
