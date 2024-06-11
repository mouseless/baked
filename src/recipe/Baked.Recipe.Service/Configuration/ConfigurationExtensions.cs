namespace Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{
    public static T GetRequiredValue<T>(this IConfiguration configuration, string key,
        T? defaultValue = default
    ) => (T)configuration.GetRequiredValue(typeof(T), key, defaultValue);

    public static object GetRequiredValue(this IConfiguration configuration, Type type, string key,
        object? defaultValue = default
    ) => configuration.GetValue(type, key, defaultValue) ??
           throw new InvalidOperationException($"Looked for a value {key} in Configurations, but could not found");
}