using Microsoft.Extensions.Localization;

namespace Baked.Test.ExceptionHandling;

public class LocalizationSamples(IStringLocalizer _l)
{
    public string GetLocaleString() =>
        _l["test"];
}