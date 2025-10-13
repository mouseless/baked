using Baked.Architecture;
using Baked.Cors;

namespace Baked;

public static class CorsExtensions
{
    public static void AddCors(this IList<IFeature> features, Func<CorsConfigurator, IFeature<CorsConfigurator>> configure) =>
        features.Add(configure(new()));
}