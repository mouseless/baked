using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.Orm;

public class LazyLoading : TestNfr
{
    [Test]
    public async Task Proxy_classes_serialized_correctly()
    {
        var parentResponse = await Client.PostAsync("/parents", JsonContent.Create(new { name = "test" }));
        dynamic? parent = await parentResponse.Content.Deserialize();

        await Client.PostAsync($"/parents/{parent?.id}/children", JsonContent.Create(new { }));

        var response = await Client.GetAsync($"/children");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}