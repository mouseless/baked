using Baked.Architecture;
using Baked.Cors;

namespace Baked;

public static class CorsExtensions
{
    extension(IList<IFeature> features)
    {
        public void AddCors(FeatureFunc<CorsConfigurator> configure) =>
            features.Add(configure(new()));
    }
}