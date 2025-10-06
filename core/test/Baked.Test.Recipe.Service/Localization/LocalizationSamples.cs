using Microsoft.Extensions.Localization;

namespace Baked.Test.Localization;

public class LocalizationSamples(IStringLocalizer _l)
{
    public string GetLocaleString() =>
        _l["test"];

    public string GetParameterized(string param) =>
        _l["Parameter value is '{0}'", param];
}