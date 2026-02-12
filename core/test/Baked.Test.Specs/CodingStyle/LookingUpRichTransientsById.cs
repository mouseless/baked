using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class LookingUpRichTransientsById : TestNfr
{
    [Test]
    public async Task ExposedLocate()
    {
        var response = await Client.GetAsync("/rich-transient-with-datas/1");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task TransientParameters()
    {
        var response = await Client.PostAsync("/method-samples/transient-parameters", JsonContent.Create(
            new
            {
                single = new { id = "test 1" },
                enumerable = new[] { new { id = "test 2" }, new { id = "test 3" } },
                array = new[] { new { id = "test 4" }, new { id = "test 5" } }
            }
        ));
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((int?)actual?.Count).ShouldBe(5);
        $"{actual?[0].name}".ShouldBe("test 1 name");
        $"{actual?[1].name}".ShouldBe("test 2 name");
        $"{actual?[2].name}".ShouldBe("test 3 name");
        $"{actual?[3].name}".ShouldBe("test 4 name");
        $"{actual?[4].name}".ShouldBe("test 5 name");
    }

    [Test]
    public async Task TransientParametersInQuery()
    {
        var response = await Client.GetAsync("/method-samples/transient-parameters" +
            "?singleId=test+1" +
            "&enumerableIds=test+2&enumerableIds=test+3" +
            "&arrayIds=test+4&arrayIds=test+5"
        );
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((int?)actual?.Count).ShouldBe(5);
        $"{actual?[0].name}".ShouldBe("test 1 name");
        $"{actual?[1].name}".ShouldBe("test 2 name");
        $"{actual?[2].name}".ShouldBe("test 3 name");
        $"{actual?[3].name}".ShouldBe("test 4 name");
        $"{actual?[4].name}".ShouldBe("test 5 name");
    }

    [Test]
    public async Task RecordWithRichTransient()
    {
        var response = await Client.PostAsync("/method-samples/record-with-rich-transient", JsonContent.Create(
            new
            {
                record = new
                {
                    single = new { id = "test 1" },
                    enumerable = new[] { new { id = "test 2" }, new { id = "test 3" } },
                    array = new[] { new { id = "test 4" }, new { id = "test 5" } },
                }
            }
        ));
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((int?)actual?.Count).ShouldBe(5);
        $"{actual?[0].name}".ShouldBe("test 1 name");
        $"{actual?[1].name}".ShouldBe("test 2 name");
        $"{actual?[2].name}".ShouldBe("test 3 name");
        $"{actual?[3].name}".ShouldBe("test 4 name");
        $"{actual?[4].name}".ShouldBe("test 5 name");
    }

    [Test]
    public async Task ExposedLocateAsync()
    {
        var response = await Client.GetAsync("/rich-transient-asyncs/1");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task TransientAsyncParameters()
    {
        var response = await Client.PostAsync("/method-samples/transient-async-parameters", JsonContent.Create(
            new
            {
                single = new { id = "test 1" },
                enumerable = new[] { new { id = "test 2" }, new { id = "test 3" } },
                array = new[] { new { id = "test 4" }, new { id = "test 5" } }
            }
        ));
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((int?)actual?.Count).ShouldBe(5);
        $"{actual?[0].name}".ShouldBe("test 1 name");
        $"{actual?[1].name}".ShouldBe("test 2 name");
        $"{actual?[2].name}".ShouldBe("test 3 name");
        $"{actual?[3].name}".ShouldBe("test 4 name");
        $"{actual?[4].name}".ShouldBe("test 5 name");
    }

    [Test]
    public async Task TransientAsyncParametersInQuery()
    {
        var response = await Client.GetAsync("/method-samples/transient-async-parameters" +
            "?singleId=test+1" +
            "&enumerableIds=test+2&enumerableIds=test+3" +
            "&arrayIds=test+4&arrayIds=test+5"
        );
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((int?)actual?.Count).ShouldBe(5);
        $"{actual?[0].name}".ShouldBe("test 1 name");
        $"{actual?[1].name}".ShouldBe("test 2 name");
        $"{actual?[2].name}".ShouldBe("test 3 name");
        $"{actual?[3].name}".ShouldBe("test 4 name");
        $"{actual?[4].name}".ShouldBe("test 5 name");
    }

    [Test]
    public async Task RecordWithRichTransientAsync()
    {
        var response = await Client.PostAsync("/method-samples/record-with-rich-transient-async", JsonContent.Create(
            new
            {
                record = new
                {
                    single = new { id = "test 1" },
                    enumerable = new[] { new { id = "test 2" }, new { id = "test 3" } },
                    array = new[] { new { id = "test 4" }, new { id = "test 5" } },
                }
            }
        ));
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((int?)actual?.Count).ShouldBe(5);
        $"{actual?[0].name}".ShouldBe("test 1 name");
        $"{actual?[1].name}".ShouldBe("test 2 name");
        $"{actual?[2].name}".ShouldBe("test 3 name");
        $"{actual?[3].name}".ShouldBe("test 4 name");
        $"{actual?[4].name}".ShouldBe("test 5 name");
    }
}