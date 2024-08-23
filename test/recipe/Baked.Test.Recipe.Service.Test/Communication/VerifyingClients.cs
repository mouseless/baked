using Baked.Communication;

namespace Baked.Test.Communication;

public class VerifyingClients : TestServiceSpec
{
    [Test]
    public async Task Verify_includes_parameters_only_when_given()
    {
        var client = MockMe.TheClient<ExternalSamples>();

        await client.Send(new("path", HttpMethod.Get));
        await client.Send(new("path", HttpMethod.Get), allowErrorResponse: true);
        await client.Send(new Request(UrlOrPath: "path", Method: HttpMethod.Get).AddAuthorization("token"));
        await client.Send(new("path2", HttpMethod.Get));
        await client.Send(new($"{GiveMe.AUrl()}", HttpMethod.Post, Content: new(new { content = "content" })));
        await client.Send(new($"{GiveMe.AUrl()}", HttpMethod.Post, Content: new(new { content = "another content" })));
        await client.Send(new($"{GiveMe.AUrl()}", HttpMethod.Post, Content: new(new() { { "Key", "Value" } })));

        client.VerifySent(path: "path", times: 3);
        client.VerifySent(url: "path2", times: 1);
        client.VerifySent(allowErrorResponse: true, times: 1);
        client.VerifySent(method: HttpMethod.Get, times: 4);
        client.VerifySent(method: HttpMethod.Post, times: 3);
        client.VerifySent(content: new { content = "content" }, times: 1);
        client.VerifySent(contentContains: "content", times: 2);
        client.VerifySent(header: ("Authorization", "token"), times: 1);
        client.VerifySent(excludesHeader: "Authorization", times: 6);
        client.VerifySent(form: new() { { "Key", "Value" } }, times: 1);
    }

    [Test]
    public async Task Verify_must_satisfy_all_parameters_when_given()
    {
        var client = MockMe.TheClient<ExternalSamples>();

        await client.Send(new Request("path", HttpMethod.Get).AddAuthorization("token"), allowErrorResponse: true);
        await client.Send(new Request("path", HttpMethod.Post, Content: new(new { content = "another content" })).AddAuthorization("token"), allowErrorResponse: true);
        await client.Send(new Request("path", HttpMethod.Post, Content: new(new { content = "content" })).AddAuthorization("token"), allowErrorResponse: false);
        await client.Send(new Request("path", HttpMethod.Post, Content: new(new { content = "content" })).AddAuthorization("another token"), allowErrorResponse: true);
        await client.Send(new Request("another/path", HttpMethod.Post, Content: new(new { content = "content" })).AddAuthorization("token"), allowErrorResponse: true);
        await client.Send(new Request("path", HttpMethod.Post, Content: new(new { content = "content" })).AddAuthorization("token"), allowErrorResponse: true);

        client.VerifySent(path: "path", method: HttpMethod.Post, header: ("Authorization", "token"), content: new { content = "content" }, allowErrorResponse: true, times: 1);
    }
}