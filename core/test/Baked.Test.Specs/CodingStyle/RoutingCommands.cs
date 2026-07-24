using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class RoutingCommands : TestNfr
{
    [TestCase("Put", "bulk-commanded")]
    [TestCase("Patch", "bulk-commanded")]
    [TestCase("Post", "commanded")]
    [TestCase("Get", "commanded")]
    [TestCase("Patch", "commanded")]
    [TestCase("Delete", "commanded")]
    public async Task Class_name_is_used_to_decide_http_methods_for_single_action_commandeds(string method, string action)
    {
        var response = await Client.SendAsync(new(HttpMethod.Parse(method), $"/{action}"));

        response.StatusCode.ShouldNotBe(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task Initialization_parameters_come_from_query()
    {
        var response = await Client.PostAsync("/commanded/method?query=q", JsonContent.Create(
            new { body = "b" }
        ));

        var actual = await response.Content.ReadAsStringAsync();

        actual.ShouldBe("\"q:b\"");
    }

    [Test]
    public async Task Delete_commandeds_use_query_for_both_init_and_execute_parameters()
    {
        var response = await Client.DeleteAsync("/commanded?initParam=i&executeParam=e");

        var actual = await response.Content.ReadAsStringAsync();

        actual.ShouldBe("\"i:e\"");
    }

    [Test]
    public async Task Batch_commandeds_with_one_list_argument_does_not_use_request_body_class([Values("Put", "Patch")] string method)
    {
        var response = await Client.SendAsync(new(HttpMethod.Parse(method), "/bulk-commanded")
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
        var response = await Client.PostAsync("/not-rendered-commanded/transient?query=q", JsonContent.Create(
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
        var entityResponse = await Client.PostAsync("/entities", null);
        dynamic? content = await entityResponse.Content.Deserialize();

        var response = await Client.PostAsync($"/command-with-entity?entityId={content?.id}&text=text", null);

        var actual = await response.Content.ReadAsStringAsync();
        actual.ShouldContain($"{content?.id}");
    }
}