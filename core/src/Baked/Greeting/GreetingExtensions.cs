using Baked.Architecture;
using Baked.Greeting;

namespace Baked;

public static class GreetingExtensions
{
    extension(List<IFeature> features)
    {
        public void AddGreeting(FeatureFunc<GreetingConfigurator> configure) =>
            features.Add(configure(new()));
    }
}