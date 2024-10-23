using Newtonsoft.Json;

namespace Baked.Test.CodingStyle;

public class RoutingRichTransients : TestServiceNfr
{
    [Test]
    public async Task Get()
    {
        var response = await Client.GetAsync("entity-groups/1");

        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((string?)actual?.id).ShouldBe("1");
    }

    [Test]
    public async Task Get_method()
    {
        var response = await Client.GetAsync("entity-groups/1/entities");

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
    }
}