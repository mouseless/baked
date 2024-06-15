using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class RoutingCommands : TestServiceNfr
{
    [TestCase("Post", "command")]
    [TestCase("Get", "command")]
    [TestCase("Patch", "command")]
    [TestCase("Delete", "command")]
    public async Task Class_name_is_used_to_decide_http_methods_for_single_action_commands(string method, string action)
    {
        var response = await Client.SendAsync(new(HttpMethod.Parse(method), $"/{action}"));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Initialization_parameters_come_from_query()
    {
        var response = await Client.PostAsync($"/command/transient?query=q", JsonContent.Create(
            new { body = "b" }
        ));

        var actual = await response.Content.ReadAsStringAsync();

        actual.ShouldBe("\"q:b\"");
    }

    [Test]
    public async Task Classes_must_have_an_initializer_overload_with_all_parameters_are_api_input()
    {
        var response = await Client.PostAsync($"/not-rendered-command/transient?query=q", JsonContent.Create(
            new { body = "b" }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}