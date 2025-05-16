using Baked.Architecture;
using Baked.Localization;

namespace Baked;

public static class LocalizationExtensions
{
    public static void AddLocalization(this IList<IFeature> features, Func<LocalizationConfigurator, IFeature<LocalizationConfigurator>> configure) =>
        features.Add(configure(new()));
}