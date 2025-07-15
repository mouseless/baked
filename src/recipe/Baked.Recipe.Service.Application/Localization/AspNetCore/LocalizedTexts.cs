using Baked.Ui;
using System.Globalization;

namespace Baked.Localization.AspNetCore;

public class LocalizedTexts(CultureInfo _language, ILocaleTemplate _template)
    : Dictionary<string, string>(_template)
{
    public LocalizedTexts With(string resourceDir,
        bool defaultLanguage = false
    )
    {
        var resourceFilePath = Path.Combine(resourceDir, defaultLanguage ? $"locale.restext" : $"locale.{_language.Name}.restext");
        if (File.Exists(resourceFilePath))
        {

            using (StreamReader reader = new(resourceFilePath))
            {
                string? line = reader.ReadLine();
                while (line != null)
                {
                    if (!string.IsNullOrWhiteSpace(line.Trim()))
                    {
                        var keyValue = line.Split('=', StringSplitOptions.TrimEntries);
                        if (ContainsKey(keyValue[0]))
                        {
                            this[keyValue[0]] = keyValue[1];
                        }
                    }

                    line = reader.ReadLine();
                }
            }
        }

        return this;
    }
}