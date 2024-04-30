using System.Net;
using System.Net.Http.Json;

namespace Do.Test.CodingStyle;

public class GettingEntityByUniqueProperty : TestServiceNfr
{
    [Test]
    public async Task Unique()
    {
        await Client.PostAsync("/entities", JsonContent.Create(
            new { unique = "test" }
        ));

        var response = await Client.GetAsync("/entities/test");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}