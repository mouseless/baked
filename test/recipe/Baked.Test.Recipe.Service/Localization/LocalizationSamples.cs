using Baked.Localization;

namespace Baked.Test.ExceptionHandling;

public class LocalizationSamples(ILocalizer _l)
{
    public string GetLocaleString() =>
        _l["test"];
}