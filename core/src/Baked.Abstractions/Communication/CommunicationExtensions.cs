using System.Net;

namespace Baked;

public static class CommunicationExtensions
{
    extension(Uri uri)
    {
        public Uri WithQuery(Dictionary<string, string> parameters)
        {
            var builder = new UriBuilder(uri);

            if (!string.IsNullOrEmpty(builder.Query))
            {
                builder.Query += "&";
            }

            builder.Query += parameters.ToQueryString();

            return builder.Uri;
        }
    }

    extension(Dictionary<string, string> parameters)
    {
        public string ToFormBody() =>
            parameters.ToQueryString();

        public string ToQueryString() =>
            string.Join("&", parameters.Select(pair => $"{pair.Key}={WebUtility.UrlEncode(pair.Value)}"));
    }
}