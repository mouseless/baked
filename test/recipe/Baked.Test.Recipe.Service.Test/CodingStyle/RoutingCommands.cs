using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class RoutingCommands : TestServiceNfr
{
    [TestCase("Put", "bulk-command")]
    [TestCase("Patch", "bulk-command")]
    [TestCase("Post", "command")]
    [TestCase("Get", "command")]
    [TestCase("Patch", "command")]
    [TestCase("Delete", "command")]
    public async Task Class_name_is_used_to_decide_http_methods_for_single_action_commands(string method, string action)
    {
        var response = await Client.SendAsync(new(HttpMethod.Parse(method), $"/{action}"));

        response.StatusCode.ShouldNotBe(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task Initialization_parameters_come_from_query()
    {
        var response = await Client.PostAsync("/command/transient?query=q", JsonContent.Create(
            new { body = "b" }
        ));

        var actual = await response.Content.ReadAsStringAsync();

        actual.ShouldBe("\"q:b\"");
    }

    [Test]
    public async Task Delete_commands_use_query_for_both_init_and_execute_parameters()
    {
        var response = await Client.DeleteAsync("/command?initParam=i&executeParam=e");

        var actual = await response.Content.ReadAsStringAsync();

        actual.ShouldBe("\"i:e\"");
    }

    [Test]
    public async Task Batch_commands_with_one_list_argument_does_not_use_request_body_class([Values("Put", "Patch")] string method)
    {
        var response = await Client.SendAsync(new(HttpMethod.Parse(method), "/bulk-command")
        {
            Content = JsonContent.Create(new[]
            {
                new { name = "a" },
                new { name = "b" },
                new { name = "c" },
            })
        });

        var actual = await response.Content.ReadAsStringAsync();

        actual.ShouldBe("\"a:b:c\"");
    }

    [Test]
    public async Task Classes_must_have_an_initializer_overload_with_all_parameters_are_api_input()
    {
        var response = await Client.PostAsync("/not-rendered-command/transient?query=q", JsonContent.Create(
            new { body = "b" }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task Initialization_parameters_can_be_rich_transient()
    {
        var response = await Client.PostAsync("/command-with-rich-transient?transientId=1", null);

        var actual = await response.Content.ReadAsStringAsync();

        actual.ShouldContain("\"id\":\"1\"");
    }

    [Test]
    public async Task Initialization_parameters_can_be_entity()
    {
        var entityResponse = await Client.PostAsync("/parents", JsonContent.Create(
            new { name = "Parent" }
        ));
        dynamic? content = JsonConvert.DeserializeObject(await entityResponse.Content.ReadAsStringAsync());

        var response = await Client.PostAsync($"/command-with-entity?parentId={content?.id}&text=text", null);

        var actual = await response.Content.ReadAsStringAsync();

        actual.ShouldContain($"{content?.id}");
    }
}