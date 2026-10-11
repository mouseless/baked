using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class BindingBuiltInTypes : TestNfr
{
    [Test]
    public async Task BuiltInTypeParameters()
    {
        var response = await Client.PostAsync("/method-samples/built-in-type-parameters", JsonContent.Create(
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
    public async Task BuiltInTypeListParameters()
    {
        var response = await Client.PostAsync("/method-samples/built-in-type-list-parameters", JsonContent.Create(
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