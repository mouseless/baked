using Microsoft.Extensions.Configuration;

namespace Do.Configuration;

public class Setting<T>
{
    public static implicit operator T(Setting<T> setting) => setting.GetValue();
    public static implicit operator Setting<T>(T value) => Settings.Constant(value);

    readonly Func<IConfiguration> _getConfiguration;
    readonly string? _key;
    readonly T? _defaultValue;

    internal Setting(T defaultValue) : this(default!, default, defaultValue) { }
    internal Setting(Func<IConfiguration> getConfiguration, string? key, T? defaultValue) =>
        (_getConfiguration, _key, _defaultValue) = (getConfiguration, key, defaultValue);

    public T GetValue() =>
        (_key is not null ? _getConfiguration().GetValue(_key, _defaultValue) : _defaultValue) ??
        throw new ConfigurationRequiredException($"Config required for {_key}");

    public T? GetSection() =>
        _key is not null ? _getConfiguration().GetSection(_key).Get<T>() ?? _defaultValue :
        throw new ConfigurationRequiredException($"Config required for {_key}");
}