using Baked.Localization;

namespace Baked.Test.ExceptionHandling;

public class LocalizationSamples(ILocalizer _localizer)
{
    public string ReadCultureDataFromQueryOrHeader() =>
        _localizer["test"];
}