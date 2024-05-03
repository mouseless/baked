using System.Net;

namespace Do.Test.CodingStyle;

public class RedirectingUriActions : TestServiceNfr
{
    [Test]
    public async Task Get()
    {
        var response = await Client.GetAsync("/redirect-samples/callback?uri=https://test.com/");

        response.StatusCode.ShouldBe(HttpStatusCode.Redirect);
        response.Headers.Location.ShouldBe("https://test.com/");
    }

    [Test]
    public async Task Post()
    {
        var response = await Client.PostAsync("/redirect-samples/form-post?uri=https://test.com/", new FormUrlEncodedContent(
            new Dictionary<string, string>() {
                { "key", "value" }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.Redirect);
        response.Headers.Location.ShouldBe("https://test.com/?key=value");
    }
}