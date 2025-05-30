using Baked.Ui;
using Humanizer;

namespace Baked.Localization.AspNetCore;

public class LocalizationPlugin : IPlugin
{
    public string Name => nameof(LocalizationPlugin).Replace("Plugin", string.Empty).Camelize();
    public IEnumerable<SupportedLanguage> SupportedLanguages { get; init; } = [];
}