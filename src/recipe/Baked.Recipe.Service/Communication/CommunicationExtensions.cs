using System.Net;

namespace Baked;

public static class CommunicationExtensions
{
    public static Uri WithQuery(this Uri uri, Dictionary<string, string> parameters)
    {
        var builder = new UriBuilder(uri);

        if (!string.IsNullOrEmpty(builder.Query))
        {
            builder.Query += "&";
        }

        builder.Query += parameters.ToQueryString();

        return builder.Uri;
    }

    public static string ToFormBody(this Dictionary<string, string> parameters) =>
        parameters.ToQueryString();

    public static string ToQueryString(this Dictionary<string, string> parameters) =>
        string.Join("&", parameters.Select(pair => $"{pair.Key}={WebUtility.UrlEncode(pair.Value)}"));
}