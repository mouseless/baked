using Baked.Communication;
using Baked.Communication.Mock;
using Baked.Testing;
using Moq;
using System.Net;

namespace Baked;

public static class MockCommunicationExtensions
{
    public static MockCommunicationFeature Mock(this CommunicationConfigurator _, Action<DefaultResponseBuilder>? defaultResponses = default) =>
        new(defaultResponses ?? (_ => { }));

    public static IClient<T> TheClient<T>(this Mocker mockMe,
        string? url = default,
        string? path = default,
        string? urlEndsWith = default,
        object? response = default,
        string? responseString = default,
        HttpStatusCode? statusCode = default,
        Exception? throws = default,
        List<object>? responses = default,
        bool? emptyResponse = default
    )
    {
        statusCode ??= HttpStatusCode.OK;

        var mock = Moq.Mock.Get(mockMe.Spec.GiveMe.The<IClient<T>>());

        var setup = () => mock.Setup(c =>
            c.Send(It.Is<Request>(r =>
                (url == default || r.UrlOrPath == url) &&
                (path == default || r.UrlOrPath == path) &&
                (urlEndsWith == default || r.UrlOrPath.EndsWith(urlEndsWith))
            ),
            It.IsAny<bool>()
        ));

        if (throws is not null)
        {
            setup().ThrowsAsync(throws);
        }
        else if (response is not null)
        {
            setup().ReturnsAsync(new Response(statusCode.GetValueOrDefault(), response.ToJsonString()));
        }
        else if (responseString is not null)
        {
            setup().ReturnsAsync(new Response(statusCode.GetValueOrDefault(), responseString));
        }
        else if (responses is not null)
        {
            setup().ReturnsAsync(responses.Select(r => new Response(statusCode.GetValueOrDefault(), r.ToJsonString())).ToArray());
        }
        else if (emptyResponse.GetValueOrDefault())
        {
            setup().ReturnsAsync(new Response(statusCode.GetValueOrDefault(), "{}"));
        }

        return mock.Object;
    }

    public static void VerifySent<T>(this IClient<T> client,
        string? path = default,
        string? url = default,
        HttpMethod? method = default,
        object? content = default,
        string? contentContains = default,
        Dictionary<string, string>? form = default,
        (string key, string value)? header = default,
        string? excludesHeader = default,
        int? times = default,
        bool? allowErrorResponse = default
    ) => Moq.Mock.Get(client).Verify(
        c => c.Send(It.Is<Request>(r =>
                (path == default || r.UrlOrPath == path) &&
                (url == default || r.UrlOrPath == url) &&
                (method == default || r.Method == method) &&
                (content == default || new Content(content, null).Equals(r.Content)) &&
                (contentContains == default || r.Content != null && r.Content.Body.Contains(contentContains)) &&
                (form == default || new Content(form).Equals(r.Content)) &&
                (!header.HasValue || r.Headers[header.GetValueOrDefault().key] == header.GetValueOrDefault().value) &&
                (excludesHeader == default || !r.Headers.ContainsKey(excludesHeader))
            ),
            It.Is<bool>(aer => allowErrorResponse == default || aer == allowErrorResponse.GetValueOrDefault())
        ),
        times is null ? Times.AtLeastOnce() : Times.Exactly(times.Value)
    );

    public static void VerifyNotSent<T>(this IClient<T> client) =>
        Moq.Mock.Get(client).Verify(c => c.Send(It.IsAny<Request>(), It.IsAny<bool>()), Times.Never());

    public static void VerifyNoContentIsSent<T>(this IClient<T> client) =>
        Moq.Mock.Get(client).Verify(c => c.Send(It.Is<Request>(r => r.Content == null), It.IsAny<bool>()));
}