using Baked.Localization.AspNetCore;

namespace Baked.Ui;

public class LocalizationDescriptor
{
    public string DefaultLanguage { get; init; } = "en";
    public IEnumerable<SupportedLanguage> SupportedLanguages { get; init; } = [new("en", "English")];
}