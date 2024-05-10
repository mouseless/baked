using System.Net;

namespace Do.Test.RestApi;

public class RemovingPartsFromRoutes : TestServiceNfr
{
    [TestCase("Post", "class/method")]
    [TestCase("Post", "command/transient")]
    public async Task Does_not_remove_route_parts_if_match_is_part_of_a_word(string method, string path)
    {
        var response = await Client.SendAsync(new(HttpMethod.Parse(method), path));

        response.StatusCode.ShouldNotBe(HttpStatusCode.NotFound);
    }
}