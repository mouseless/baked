using System.Net;
using System.Net.Http.Json;

namespace Do.Test.CodingStyle;

public class RoutingEntitySubclasses : TestServiceNfr
{
    [TestCase("type-a", "TypeA")]
    [TestCase("type-b", "TypeB")]
    [Ignore("failing")]
    public async Task Subclasses_are_served_under_entity_routes(string route, string unique)
    {
        await Client.PostAsync("/entities", JsonContent.Create(
            new { unique }
        ));

        var response = await Client.PostAsync($"/entities/{route}/operate-on-{route}", new StringContent(string.Empty));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    [Ignore("not implemented")]
    public void Subclasses_accept_post_resource_under_entity_slash_type_route() => throw new();
}