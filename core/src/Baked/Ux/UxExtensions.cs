using Baked.Architecture;
using Baked.Ux;

namespace Baked;

public static class UxExtensions
{
    extension(List<IFeature> features)
    {
        public void AddUx(IEnumerable<FeatureFunc<UxConfigurator>> configures) =>
            features.AddRange(configures.Select(configure => configure(new())));

        public void AddUxes(IEnumerable<FeatureFunc<UxConfigurator>> configures) =>
            features.AddUx(configures);
    }
}