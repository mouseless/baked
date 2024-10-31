
using System.Net;

namespace Baked.Test.CodingStyle;

public class RoutingByNamespace : TestServiceNfr
{
    [TestCase("Post", "coding-style/namespace-as-route/route-sample/method")]
    public async Task Namespace_is_used_as_base_route_for_the_methods_under_certain_namespace(string method, string action)
    {
        var response = await Client.SendAsync(new(HttpMethod.Parse(method), $"/{action}"));

        response.StatusCode.ShouldNotBe(HttpStatusCode.NotFound);
    }
}