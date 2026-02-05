using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class LookingUpRichTransientsById : TestNfr
{
    [Test]
    public async Task TransientParameters()
    {
        var response = await Client.PostAsync("/method-samples/transient-parameters", JsonContent.Create(
            new
            {
                transientId = "1"
            }
        ));
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        $"{actual?.id}".ShouldBe("1");
    }

    [Test]
    public async Task TransientAsyncParameters()
    {
        var response = await Client.PostAsync("/method-samples/transient-async-parameters", JsonContent.Create(
            new
            {
                transientAsyncId = "1"
            }
        ));
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        $"{actual?.id}".ShouldBe("1");
    }

    [Test]
    public async Task TransientListParameters()
    {
        var response = await Client.PostAsync("/method-samples/transient-list-parameters", JsonContent.Create(
            new
            {
                transientIds = new[] { "1", "2" },
                otherTransientIds = new[] { "3", "4" },
            }
        ));
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((int?)actual?.Count).ShouldBe(4);
        $"{actual?[0].id}".ShouldBe("1");
    }

    [Test]
    public async Task TransientAsyncListParameters()
    {
        var response = await Client.PostAsync("/method-samples/transient-async-list-parameters", JsonContent.Create(
            new
            {
                transientAsyncIds = new[] { "1", "2" },
                otherTransientAsyncIds = new[] { "3", "4" },
            }
        ));
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((int?)actual?.Count).ShouldBe(4);
        $"{actual?[0].id}".ShouldBe("1");
    }
}