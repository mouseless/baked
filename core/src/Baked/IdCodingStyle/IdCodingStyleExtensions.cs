using Baked.Architecture;

namespace Baked.IdCodingStyle;

public static class IdCodingStyleExtensions
{
    public static void AddIdCodingStyle(this List<IFeature> features, Func<IdCodingStyleConfigurator, IFeature<IdCodingStyleConfigurator>> configure) =>
        features.Add(configure(new()));
}