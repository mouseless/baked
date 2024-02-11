namespace Do.Communication.Http;

public record ClientConfig(
    Uri? BaseAddress = default,
    Dictionary<string, string>? DefaultHeaders = default
);

public static class DictionaryExtensions
{
    public static Dictionary<string, string> OverrideDictionary(this Dictionary<string, string>? overrider, Dictionary<string, string>? source)
    {
        overrider ??= [];
        source ??= [];

        foreach (var (key, value) in overrider)
        {
            source[key] = value;
        }

        return source;
    }
}
