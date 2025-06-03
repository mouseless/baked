using Baked.Localization.AspNetCore;

namespace Baked.Ui;

public class I18nDescriptor
{
    public IEnumerable<SupportedLanguage>? SupportedLanguages { get; set; }
}