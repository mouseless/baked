using Baked.Ui.Configuration;

namespace Baked.Localization.Dotnet;

public record LocalizationPlugin : ModulePluginBase
{
    public IEnumerable<Language> SupportedLanguages { get; init; } = [];
}