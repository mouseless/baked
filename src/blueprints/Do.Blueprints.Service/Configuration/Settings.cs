using Microsoft.Extensions.Configuration;
using Do.Configuration;

namespace Do;

public class Settings
{
    static IConfigurationRoot _configuration = default!;
    internal static void SetConfiguration(IConfigurationRoot configuration) => _configuration = configuration;

    internal static Setting<T> Constant<T>(T value) => new Setting<T>(value);

    public static Setting<T> Required<T>(string key) => new Setting<T>(() => _configuration, key, default);
    public static Setting<T> Optional<T>(string key, T defaultValue) => new Setting<T>(() => _configuration, key, defaultValue);
}
