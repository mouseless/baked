using Baked.Localization;
using Baked.Localization.Dotnet;
using System.Globalization;

namespace Baked;

public static class DotnetLocalizationExtensions
{
    public static DotnetLocalizationFeature Dotnet(this LocalizationConfigurator _,
        CultureInfo? language = null,
        IEnumerable<CultureInfo>? otherLanguages = null
    )
    {
        language ??= new("en");

        return new(language, otherLanguages);
    }
}