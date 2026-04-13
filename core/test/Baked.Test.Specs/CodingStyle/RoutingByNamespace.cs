using System.Net;

namespace Baked.Test.CodingStyle;

public class RoutingByNamespace : TestNfr
{
    [TestCase("Get", "coding-style/namespace-as-route/route-samples/test")]
    [TestCase("Post", "coding-style/namespace-as-route/route-samples/test/method")]
    public async Task Namespace_is_used_as_base_route_for_the_methods_under_certain_namespace(string method, string action)
    {
        var response = await Client.SendAsync(new(HttpMethod.Parse(method), $"/{action}"));

        response.StatusCode.ShouldNotBe(HttpStatusCode.NotFound);
    }
}