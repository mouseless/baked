using Baked.Architecture;
using Baked.Id;

namespace Baked;

public static class IdExtensions
{
    public static void AddId(this List<IFeature> features, Func<IdConfigurator, IFeature<IdConfigurator>> configure) =>
        features.Add(configure(new()));
}