using Microsoft.Extensions.Configuration;

namespace Do.Configuration;

public static class ConfigurationExtensions
{
    public static T GetRequiredValue<T>(this IConfiguration configuration, string key,
        T? defaultValue = default
    ) => configuration.GetValue<T>(key) ?? defaultValue ??
           throw new InvalidOperationException($"Looked for a value {key} in Configurations, but could not found");
}
