using System.Net.Http.Json;

namespace Do.Test.CodingStyle;

public class RoutingCommands : TestServiceNfr
{
    [Test]
    public async Task Initialization_parameters_come_from_query([Values("execute", "execute-alternative")] string action)
    {
        var response = await Client.PostAsync($"/command/{action}?query=q", JsonContent.Create(
            new { body = "b" }
        ));

        var actual = await response.Content.ReadAsStringAsync();

        actual.ShouldBe("q:b");
    }

    [Test]
    public async Task Action_name_is_hidden_when_command_has_only_one_method()
    {
        var response = await Client.PostAsync($"/execute-command?query=q", JsonContent.Create(
            new { body = "b" }
        ));

        var actual = await response.Content.ReadAsStringAsync();

        actual.ShouldBe("q:b");
    }
}