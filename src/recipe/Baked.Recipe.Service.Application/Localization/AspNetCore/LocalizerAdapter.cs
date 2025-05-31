
using Microsoft.Extensions.Localization;

namespace Baked.Localization.AspNetCore;

public class LocalizerAdapter(IStringLocalizer _localizer) : ILocalizer
{
    public string this[string key] => _localizer[key];

    public string this[string key, params object[] arguments] => _localizer[key, arguments];
}