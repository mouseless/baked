using System.Net;
using System.Net.Http.Json;

namespace Do.Test.CodingStyle;

public class RoutingEntitySubclasses : TestServiceNfr
{
    [TestCase("a", "A")]
    [TestCase("b", "B")]
    public async Task Subclasses_are_served_under_entity_routes(string route, string type)
    {
        await Client.PostAsync("/typed-entities", JsonContent.Create(
            new { type }
        ));

        var response = await Client.PostAsync($"/typed-entities/{route}/operate-on-{route}", null);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    [Ignore("not implemented")]
    public void Subclasses_accept_post_resource_under_entity_slash_type_route() => throw new();
}