using Baked.Architecture;
using Baked.Ux;

namespace Baked;

public static class UxExtensions
{
    public static void AddUx(this List<IFeature> features, IEnumerable<Func<UxConfigurator, IFeature<UxConfigurator>>> configures) =>
        features.AddRange(configures.Select(configure => configure(new())));

    public static void AddUxes(this List<IFeature> features, IEnumerable<Func<UxConfigurator, IFeature<UxConfigurator>>> configures) =>
        features.AddUx(configures);
}