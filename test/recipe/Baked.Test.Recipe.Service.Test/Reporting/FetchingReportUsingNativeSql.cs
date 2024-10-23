using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Baked.Test.Runtime;

public class FetchingReportUsingNativeSql : TestServiceNfr
{
    [Test]
    public async Task Loads_query_from_resource_and_fetches_data_from_db()
    {
        await Client.PostAsync("/entities", JsonContent.Create(new { @string = "test-1" }));
        await Client.PostAsync("/entities", JsonContent.Create(new { @string = "test-1" }));
        await Client.PostAsync("/entities", JsonContent.Create(new { @string = "test-2" }));

        var response = await Client.GetAsync("report-samples/entity?name=test");
        dynamic? content = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((int?)content?[0].count).ShouldBe(2);
        ((string?)content?[0].name).ShouldBe("test-1");
        ((int?)content?[1].count).ShouldBe(1);
        ((string?)content?[1].name).ShouldBe("test-2");
    }
}