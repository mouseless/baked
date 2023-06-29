using Do.Test.Blueprints.Service.Greeting;
using Do.Test.Blueprints.Service.Greeting.WelcomePage;

namespace Do;

public static class WelcomePageGreetingExtensions
{
    public static WelcomePageGreeting WelcomePage(this GreetingConfigurator source,
        string? path = default
    ) => new(path ?? "/");
}
