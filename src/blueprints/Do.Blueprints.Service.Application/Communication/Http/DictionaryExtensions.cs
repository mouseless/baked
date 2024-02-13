namespace System;

internal static class DictionaryExtensions
{
    internal static Dictionary<string, string> Merge(this Dictionary<string, string>? to, Dictionary<string, string>? source)
    {
        to ??= [];
        source ??= [];

        foreach (var (key, value) in source)
        {
            if (!to.ContainsKey(key))
            {
                to[key] = value;
            }
        }

        return to;
    }
}

