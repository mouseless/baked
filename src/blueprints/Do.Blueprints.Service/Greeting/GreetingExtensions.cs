using Do.Architecture;
using Do.Blueprints.Service.Greeting;

namespace Do;

public static class GreetingExtensions
{
    public static void AddGreeting(this List<IFeature> source, Func<GreetingConfigurator, IFeature> configure) => source.Add(configure(new()));
}
