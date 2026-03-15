using Baked.Architecture;
using Baked.CodingStyle;

namespace Baked;

public static class CodingStyleExtensions
{
    extension(List<IFeature> features)
    {
        public void AddCodingStyles(IEnumerable<FeatureFunc<CodingStyleConfigurator>> configures) =>
            features.AddRange(configures.Select(configure => configure(new())));
    }
}