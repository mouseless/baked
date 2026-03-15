using Baked.Architecture;
using Baked.Greeting;

namespace Baked;

public static class GreetingExtensions
{
    extension(List<IFeature> features)
    {
        public void AddGreeting(Func<GreetingConfigurator, IFeature<GreetingConfigurator>> configure) =>
            features.Add(configure(new()));
    }
}