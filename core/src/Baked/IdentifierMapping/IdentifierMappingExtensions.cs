using Baked.Architecture;
using Baked.IdentifierMapping;

namespace Baked;

public static class IdentifierMappingExtensions
{
    public static void AddIdentifierMapping(this List<IFeature> features, Func<IdentifierMappingConfigurator, IFeature<IdentifierMappingConfigurator>> configure) =>
        features.Add(configure(new()));
}