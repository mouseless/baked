namespace Do.Configuration;

public class ConfigurationRequiredException : Exception
{
    public ConfigurationRequiredException(string key) : base($"Configuration required for {key}") { }
}
