using Baked.Architecture;
using Baked.Binding;

namespace Baked;

public static class BindingExtensions
{
    extension(List<IFeature> features)
    {
        public void AddBindings(params IEnumerable<FeatureFunc<BindingConfigurator>> configures) =>
            features.AddRange(configures.Select(configure => configure(new())));
    }
}