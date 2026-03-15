using Baked.Localization;
using Baked.Localization.Dotnet;
using System.Globalization;

namespace Baked;

public static class DotnetLocalizationExtensions
{
    extension(LocalizationConfigurator _)
    {
        public DotnetLocalizationFeature Dotnet(
            CultureInfo? language = null,
            IEnumerable<CultureInfo>? otherLanguages = null
        )
        {
            language ??= new("en");

            return new(language, otherLanguages);
        }
    }
}