using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class RoutingRichTransients : TestNfr
{
    [TestCase("1")]
    [TestCase("59dfa608-9fe4-4e77-b448-a65adcfda605")]
    public async Task Get(string id)
    {
        var response = await Client.GetAsync($"rich-transient-with-datas/{id}");

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
        ((string?)actual?.id).ShouldBe(id);
    }

    [TestCase("rich-transient-with-datas/1/method")]
    [TestCase("rich-transient-no-datas/1/method")]
    public async Task Post(string path)
    {
        var response = await Client.PostAsync(path, JsonContent.Create(new { text = "text" }));

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        dynamic? actual = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
        ((string?)actual).ShouldBe("text");
    }

    [Test]
    public async Task Rich_transient_with_no_public_data_has_no_get_resource()
    {
        var response = await Client.GetAsync("rich-transient-no-datas/1");

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
    }
}