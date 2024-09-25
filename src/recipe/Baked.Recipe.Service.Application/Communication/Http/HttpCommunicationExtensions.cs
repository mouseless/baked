using Baked.Communication;
using Baked.Communication.Http;

namespace Baked;

public static class HttpCommunicationExtensions
{
    public static HttpCommunicationFeature Http(this CommunicationConfigurator _) =>
        new();

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