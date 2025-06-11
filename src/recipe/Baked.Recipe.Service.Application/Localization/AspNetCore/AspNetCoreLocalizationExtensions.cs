using Baked.Localization;
using Baked.Localization.AspNetCore;
using System.Globalization;

namespace Baked;

public static class AspNetCoreLocalizationExtensions
{
    public static AspNetCoreLocalizationFeature AspNetCore(this LocalizationConfigurator _,
        CultureInfo? language = null,
        IEnumerable<CultureInfo>? otherLanguages = null
    )
    {
        language ??= new("en");

        return new(language, otherLanguages);
    }
}