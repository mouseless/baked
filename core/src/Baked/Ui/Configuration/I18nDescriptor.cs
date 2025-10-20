using Baked.Localization;

namespace Baked.Ui.Configuration;

public record I18nDescriptor
{
    public Language? DefaultLanguage { get; set; }
    public List<Language> SupportedLanguages { get; } = [];
}