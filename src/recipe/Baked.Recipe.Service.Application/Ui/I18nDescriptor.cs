using Baked.Localization.AspNetCore;

namespace Baked.Ui;

public record I18nDescriptor
{
    public Language? DefaultLanguage { get; set; }
    public List<Language> SupportedLanguages { get; } = [];
}