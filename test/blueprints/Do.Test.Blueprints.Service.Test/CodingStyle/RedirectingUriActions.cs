using System.Net;

namespace Do.Test.CodingStyle;

public class RedirectingUriActions : TestServiceNfr
{
    [Test]
    public async Task Get([Values(["callback", "callback-async"])] string route)
    {
        var response = await Client.GetAsync($"/redirect-samples/{route}?uri=https://test.com/");

        response.StatusCode.ShouldBe(HttpStatusCode.Redirect);
        response.Headers.Location.ShouldBe("https://test.com/");
    }

    [Test]
    public async Task Post([Values(["form-post", "form-post-async"])] string route)
    {
        var response = await Client.PostAsync($"/redirect-samples/{route}?uri=https://test.com/", new FormUrlEncodedContent(
            new Dictionary<string, string>() {
                { "key", "value" }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.Redirect);
        response.Headers.Location.ShouldBe("https://test.com/?key=value");
    }
}