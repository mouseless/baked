using Baked.Ui.Configuration;

namespace Baked.Localization.Dotnet;

public record LocalizationPlugin()
    : PluginBase(Module: true)
{
    public IEnumerable<Language> SupportedLanguages { get; init; } = [];
}