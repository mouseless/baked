using System.Globalization;
using Baked.Localization;
using Baked.Localization.AspNetCore;
using Baked.Runtime;

namespace Baked;

public static class AspNetCoreLocalizationExtensions
{
    public static AspNetCoreLocalizationFeature AspNetCore(this LocalizationConfigurator _,
        Setting<string>? _resourceName = null,
        CultureInfo? _language = null,
        IEnumerable<CultureInfo>? _supportedLanguages = null
    )
    {
        _language ??= new("us");

        return new(_resourceName, _language, _supportedLanguages);
    }
}