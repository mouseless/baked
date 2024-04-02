using System.Net;
using System.Net.Http.Json;

namespace Do.Test.Business;

public class ExposingPublicMethods : TestServiceNfr
{
    [Test]
    public async Task Void()
    {
        var response = await Client.PostAsync("/generated/Methods/Void", new StringContent(string.Empty));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task VoidAsync()
    {
        var response = await Client.PostAsync("/generated/Methods/VoidAsync", new StringContent(string.Empty));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task PrimitiveParameters()
    {
        var response = await Client.PostAsync("/generated/Methods/PrimitiveParameters", JsonContent.Create(
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
        var response = await Client.PostAsync("/generated/Methods/PrimitiveListParameters", JsonContent.Create(
            new
            {
                strings = new[] { "a", "b" },
                ints = new[] { 1, 2 },
                dateTimes = new[] { DateTime.Now, DateTime.Today }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task EntityParameters()
    {
        var response = await Client.PostAsync("/generated/Methods/EntityParameters", JsonContent.Create(
            new
            {
                entityId = $"{Guid.NewGuid()}"
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task EntityListParameters()
    {
        var response = await Client.PostAsync("/generated/Methods/EntityListParameters", JsonContent.Create(
            new
            {
                entityIds = new[] { $"{Guid.NewGuid()}", $"{Guid.NewGuid()}" },
                otherEntityIds = new[] { $"{Guid.NewGuid()}", $"{Guid.NewGuid()}" },
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task EntityMethods()
    {
        var response = await Client.DeleteAsync($"/generated/Entity/{Guid.NewGuid()}/Delete");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
