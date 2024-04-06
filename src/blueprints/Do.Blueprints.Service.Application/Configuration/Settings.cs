using Do.Configuration;
using Microsoft.Extensions.Configuration;

namespace Do;

public class Settings
{
    static IConfigurationRoot _configurationRoot = default!;

    internal static void SetConfigurationRoot(IConfigurationRoot configurationRoot) =>
        _configurationRoot = configurationRoot;

    internal static Setting<T> Constant<T>(T value) => new Setting<T>(value);

    public static Setting<T> Required<T>(string key) => new Setting<T>(() => _configurationRoot, key, default);
    public static Setting<T> Optional<T>(string key, T defaultValue) => new Setting<T>(() => _configurationRoot, key, defaultValue);
}