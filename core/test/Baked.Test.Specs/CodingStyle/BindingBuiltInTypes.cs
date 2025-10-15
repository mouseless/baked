using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class BindingBuiltInTypes : TestNfr
{
    [Test]
    public async Task PrimitiveParameters()
    {
        var response = await Client.PostAsync("/method-samples/primitive-parameters", JsonContent.Create(
            new
            {
                @string = "string",
                @int = 42,
                dateTime = DateTime.Now
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task PrimitiveListParameters()
    {
        var response = await Client.PostAsync("/method-samples/primitive-list-parameters", JsonContent.Create(
            new
            {
                strings = new[] { "a", "b" },
                ints = new[] { 1, 2 },
                dateTimes = new[] { DateTime.Now, DateTime.Today }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}