using Baked.Localization;

namespace Baked.Test.ExceptionHandling;

public class LocalizationSamples(ILocalizer _localizer)
{
    public string GetLocaleString() =>
        _localizer["test"];
}