using Do.Architecture;
using Do.Greeting;

namespace Do;

public static class GreetingExtensions
{
    public static void AddGreeting(this List<IFeature> source, Func<GreetingConfigurator, IGreetingFeature> configure) => source.Add(configure(new()));
}
