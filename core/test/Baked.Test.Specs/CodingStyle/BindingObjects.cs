using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class BindingObjects : TestNfr
{
    [Test]
    public async Task Binds_single_object_parameter_as_request_body([Values("object", "object-async")] string route)
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

    [Test]
    public async Task Builds_request_class_when_action_has_multiple_parameters()
    {
        var response = await Client.PostAsync($"/method-samples/multiple-objects", JsonContent.Create(
            new
            {
                @object1 = new { param1 = "param1" },
                @object2 = new { param2 = "param2" }
            }
        ));
        var actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        actual.ShouldDeeplyBe(
            new
            {
                @object1 = new { param1 = "param1" },
                @object2 = new { param2 = "param2" }
            }
        );
    }
}