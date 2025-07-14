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

    public static Dictionary<string, string> ReadResxFileAsDictionary(string path)
    {
        var values = new Dictionary<string, string>();
        using (StreamReader reader = new(path))
        {
            string? line = reader.ReadLine();
            while (line != null)
            {
                if (!string.IsNullOrWhiteSpace(line.Trim()))
                {
                    var keyValue = line.Split('=', StringSplitOptions.TrimEntries);
                    values.TryAdd(keyValue[0], keyValue[1]);
                }

                line = reader.ReadLine();
            }
        }

        return values;
    }
}