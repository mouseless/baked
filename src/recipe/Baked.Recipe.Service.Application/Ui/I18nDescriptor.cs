using Baked.Localization.AspNetCore;

namespace Baked.Ui;

public record I18nDescriptor(IEnumerable<SupportedLanguage> SupportedLanguages);