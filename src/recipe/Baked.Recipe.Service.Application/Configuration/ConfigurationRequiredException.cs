namespace Baked.Configuration;

public class ConfigurationRequiredException(string _key)
    : Exception($"Configuration required for {_key}")
{ }