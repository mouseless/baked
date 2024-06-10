using System.Net;
using System.Net.Http.Json;

namespace Do.Test.Business;

public class ExposingPublicMethods : TestServiceNfr
{
    [Test]
    public async Task Void([Values("void", "void-async")] string route)
    {
        var response = await Client.PostAsync($"/method-samples/{route}", null);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Post()
    {
        var response = await Client.PostAsync($"/method-samples", null);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Put()
    {
        var response = await Client.PutAsync($"/method-samples", null);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Patch()
    {
        var response = await Client.PatchAsync($"/method-samples/string", null);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Delete()
    {
        var response = await Client.DeleteAsync($"/method-samples");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Get()
    {
        var response = await Client.GetAsync($"/method-samples/strings");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task AddChildConvention()
    {
        var response = await Client.PostAsync($"/method-samples/strings", JsonContent.Create(new { @string = "test" }));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task SetSetting()
    {
        var response = await Client.PatchAsync("/method-samples/setting", JsonContent.Create(new { value = "test" }));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}