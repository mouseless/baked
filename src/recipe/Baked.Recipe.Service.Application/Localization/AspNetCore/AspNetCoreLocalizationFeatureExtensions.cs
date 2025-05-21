using Baked.Localization;
using Baked.Localization.AspNetCore;
using Baked.Runtime;

namespace Baked;

public static class AspNetCoreLocalizationFeatureExtensions
{
    public static AspNetCoreLocalizationFeature AspNetCoreLocalization(this LocalizationConfigurator _,
        Setting<string>? _resourceName = null,
        IEnumerable<SupportedLanguage>? _supportedLanguages = null
    ) => new(_resourceName, _supportedLanguages);
}