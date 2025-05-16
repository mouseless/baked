using Baked.Localization;
using Baked.Localization.AspNetCore;

namespace Baked;

public static class AspNetCoreLocalizationFeatureExtensions
{
    public static AspNetCoreLocalizationFeature AspNetCoreLocalization(this LocalizationConfigurator _) =>
        new();
}