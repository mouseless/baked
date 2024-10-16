using Baked.Architecture;
using Baked.Resource;

namespace Baked;

public static class ResourceExtensions
{
    public static void AddResource(this List<IFeature> features, IEnumerable<Func<ResourceConfigurator, IFeature<ResourceConfigurator>>> configures) =>
        features.AddRange(configures.Select(configure => configure(new())));
}