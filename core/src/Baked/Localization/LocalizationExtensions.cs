using Baked.Architecture;
using Baked.Localization;

namespace Baked;

public static class LocalizationExtensions
{
    extension(IList<IFeature> features)
    {
        public void AddLocalization(Func<LocalizationConfigurator, IFeature<LocalizationConfigurator>> configure) =>
            features.Add(configure(new()));
    }
}