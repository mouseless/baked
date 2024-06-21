namespace System;

internal static class DictionaryExtensions
{
    internal static Dictionary<string, string> Merge(this Dictionary<string, string>? dictionary, Dictionary<string, string>? input)
    {
        dictionary ??= [];
        input ??= [];

        foreach (var (key, value) in input)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
        }

        return dictionary;
    }
}