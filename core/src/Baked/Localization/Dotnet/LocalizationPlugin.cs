using Baked.Ui;

namespace Baked.Localization.Dotnet;

public record LocalizationPlugin : PluginBase
{
    public IEnumerable<Language> SupportedLanguages { get; init; } = [];
}