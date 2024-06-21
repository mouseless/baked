using Baked.Architecture;
using Baked.Greeting;

namespace Baked;

public static class GreetingExtensions
{
    public static void AddGreeting(this List<IFeature> features, Func<GreetingConfigurator, IFeature<GreetingConfigurator>> configure) =>
        features.Add(configure(new()));
}