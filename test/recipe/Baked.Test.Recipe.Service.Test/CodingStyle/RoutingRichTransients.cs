using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class RoutingRichTransients : TestServiceNfr
{
    [Test]
    public async Task Get()
    {
        var response = await Client.GetAsync("rich-transient-with-datas/1");

        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((string?)actual?.id).ShouldBe("1");
    }

    [TestCase("rich-transient-with-datas/1/method")]
    [TestCase("rich-transient-no-datas/1/method")]
    public async Task Post(string path)
    {
        var response = await Client.PostAsync(path, JsonContent.Create(new { data = "text" }));

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
    }

    [Test]
    public async Task Rich_transient_with_no_public_data_has_no_get_resource()
    {
        var response = await Client.GetAsync("rich-transient-no-datas/1");

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
    }
}