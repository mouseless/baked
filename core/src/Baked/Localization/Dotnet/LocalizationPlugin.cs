using Baked.Ui.Configuration;

namespace Baked.Localization.Dotnet;

public record LocalizationPlugin : PluginBase
{
    public IEnumerable<Language> SupportedLanguages { get; init; } = [];
}