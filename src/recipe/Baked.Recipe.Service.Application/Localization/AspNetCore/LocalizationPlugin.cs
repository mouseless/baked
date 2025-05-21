using Baked.Ui;
using Humanizer;

namespace Baked.Localization.AspNetCore;

public class LocalizationPlugin : IPlugin
{
    public string Name => nameof(LocalizationPlugin).Replace("Plugin", string.Empty).Camelize();
    public string DefaultLanguage { get; init; } = "en";
    public IEnumerable<SupportedLanguage> SupportedLanguages { get; init; } = [new("en", "English")];

}

public record SupportedLanguage(string Code, string Name);