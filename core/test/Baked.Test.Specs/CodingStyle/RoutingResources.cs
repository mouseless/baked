using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class RoutingResources : TestNfr
{
    [TestCase("1")]
    [TestCase("59dfa608-9fe4-4e77-b448-a65adcfda605")]
    public async Task Get(string id)
    {
        var response = await Client.GetAsync($"resource-with-datas/{id}");

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        dynamic? actual = await response.Content.Deserialize();
        ((string?)actual?.id).ShouldBe(id);
    }

    [TestCase("resource-with-datas/1/method")]
    [TestCase("resource-no-datas/1/method")]
    public async Task Post(string path)
    {
        var response = await Client.PostAsync(path, JsonContent.Create(new { text = "text" }));

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        dynamic? actual = await response.Content.Deserialize();
        ((string?)actual).ShouldBe("text");
    }

    [Test]
    public async Task Rich_transient_with_no_public_data_has_no_get_resource()
    {
        var response = await Client.GetAsync("resource-no-datas/1");

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
    }
}