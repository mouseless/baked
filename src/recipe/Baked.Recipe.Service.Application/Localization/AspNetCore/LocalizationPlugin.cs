using Baked.Ui;

namespace Baked.Localization.AspNetCore;

public record LocalizationPlugin : PluginBase
{
    public IEnumerable<Language> SupportedLanguages { get; init; } = [];
}