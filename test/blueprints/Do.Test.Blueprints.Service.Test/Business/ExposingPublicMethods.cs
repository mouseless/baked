using System.Net;

namespace Do.Test.Business;

public class ExposingPublicMethods : TestServiceNfr
{
    [Test]
    public async Task Void([Values("void", "void-async")] string route)
    {
        var response = await Client.PostAsync($"/method-samples/{route}", new StringContent(string.Empty));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    [Ignore("not tested")]
    public void Post() =>
        throw new("fail");

    [Test]
    [Ignore("not tested")]
    public void Get() =>
        throw new("fail");

    [Test]
    [Ignore("not tested")]
    public void Put() =>
        throw new("fail");

    [Test]
    [Ignore("not tested")]
    public void Patch() =>
        throw new("fail");

    [Test]
    [Ignore("not tested")]
    public void Delete() =>
        throw new("fail");

    [Test]
    [Ignore("not tested")]
    public void AddStrings() =>
        throw new("fail");
}