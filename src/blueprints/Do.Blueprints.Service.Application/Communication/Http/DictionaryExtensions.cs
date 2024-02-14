namespace System;

internal static class DictionaryExtensions
{
    internal static Dictionary<string, string> Merge(this Dictionary<string, string>? source, Dictionary<string, string>? input)
    {
        source ??= [];
        input ??= [];

        foreach (var (key, value) in input)
        {
            if (!source.ContainsKey(key))
            {
                source[key] = value;
            }
        }

        return source;
    }
}

