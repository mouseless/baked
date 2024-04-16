using System.Net;
using System.Net.Http.Json;

namespace Do.Test.Business;

public class ExposingPublicMethods : TestServiceNfr
{
    [Test]
    public async Task Void()
    {
        var response = await Client.PostAsync("/method-samples/void", new StringContent(string.Empty));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task VoidAsync()
    {
        var response = await Client.PostAsync("/method-samples/void-async", new StringContent(string.Empty));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

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

    [Test]
    public async Task EntityParameters()
    {
        var response = await Client.PostAsync("/method-samples/entity-parameters", JsonContent.Create(
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
        var response = await Client.PostAsync("/method-samples/entity-list-parameters", JsonContent.Create(
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
        var response = await Client.DeleteAsync($"/entity/{Guid.NewGuid()}/delete");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}