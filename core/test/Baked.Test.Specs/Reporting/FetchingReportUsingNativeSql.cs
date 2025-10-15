using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.Runtime;

public class FetchingReportUsingNativeSql : TestNfr
{
    [Test]
    public async Task Loads_query_from_resource_and_fetches_data_from_db()
    {
        await Client.PostAsync("/entities", JsonContent.Create(new { @string = "test-1" }));
        await Client.PostAsync("/entities", JsonContent.Create(new { @string = "test-1" }));
        await Client.PostAsync("/entities", JsonContent.Create(new { @string = "test-2" }));

        var response = await Client.GetAsync("report-samples/entity?string=test");
        dynamic? content = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((int?)content?[0].count).ShouldBe(2);
        ((string?)content?[0].@string).ShouldBe("test-1");
        ((int?)content?[1].count).ShouldBe(1);
        ((string?)content?[1].@string).ShouldBe("test-2");
    }

    [Test]
    public async Task Throws_query_not_found_when_attempts_to_execute_a_non_existing_query()
    {
        var response = await Client.GetAsync("report-samples/non-existing");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}