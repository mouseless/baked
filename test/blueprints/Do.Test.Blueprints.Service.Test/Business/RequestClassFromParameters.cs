using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Do.Test.Business;
public class RequestClassFromParameters : TestServiceNfr
{
    [Test]
    public async Task FormRequestClass()
    {
        var response = await Client.PostAsync($"/method-samples/request-class", JsonContent.Create(
            new
            {
                @string = "string",
                @int = 1
            }
        ));
        var actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        actual.ShouldDeeplyBe(
            new
            {
                @string = "string",
                @int = 1
            }
        );
    }
}