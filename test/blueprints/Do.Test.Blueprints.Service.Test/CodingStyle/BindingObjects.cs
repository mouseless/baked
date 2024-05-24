using System.Net.Http.Json;
using Newtonsoft.Json;

namespace Do.Test.CodingStyle;

public class BindingObjects : TestServiceNfr
{
    [Test]
    public async Task Object([Values("object", "object-async")] string route)
    {
        var response = await Client.PostAsync($"/method-samples/{route}", JsonContent.Create(
            new
            {
                any = "object",
                canBe = "sent"
            }
        ));
        var actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        actual.ShouldDeeplyBe(
            new
            {
                any = "object",
                canBe = "sent"
            }
        );
    }
}