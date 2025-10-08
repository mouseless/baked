using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class BindingRecords : TestServiceNfr
{
    [Test]
    public async Task RecordParameters()
    {
        var response = await Client.PostAsync("/method-samples/record-parameters", JsonContent.Create(
            new
            {
                record = new { text = "test", numeric = 42 }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task RecordListParameters()
    {
        var response = await Client.PostAsync("/method-samples/record-list-parameters", JsonContent.Create(
            new
            {
                records = new[]
                {
                    new { text = "item 1", numeric = 42 },
                    new { text = "item 2", numeric = 24 }
                }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}