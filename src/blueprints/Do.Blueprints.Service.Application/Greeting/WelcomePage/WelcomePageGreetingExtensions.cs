using Do.Greeting;
using Do.Greeting.WelcomePage;

namespace Do;

public static class WelcomePageGreetingExtensions
{
    public static WelcomePageGreetingFeature WelcomePage(this GreetingConfigurator source,
        string? path = default
    ) => new(path ?? "/");
}
