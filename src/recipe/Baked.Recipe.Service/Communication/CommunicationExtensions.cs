using System.Net;

namespace Baked.Communication;

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

    public static StatusCode ToStatusCode(this HttpStatusCode httStatusCode) =>
        (int)httStatusCode switch
        {
            var c when c < 400 => StatusCode.Success,
            var c when c < 500 => StatusCode.Handled,
            _ => StatusCode.Unhandled
        };
}