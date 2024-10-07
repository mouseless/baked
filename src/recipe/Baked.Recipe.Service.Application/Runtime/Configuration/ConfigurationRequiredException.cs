namespace Baked.Runtime.Configuration;

public class ConfigurationRequiredException(string _key)
    : Exception($"Configuration required for {_key}")
{ }