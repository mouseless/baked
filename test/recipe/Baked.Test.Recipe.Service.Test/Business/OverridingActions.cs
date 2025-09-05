using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.Business;

public class OverridingActions : TestServiceNfr
{
    [Test]
    public async Task Action_route_parts_can_be_overriden()
    {
        var response = await Client.PatchAsync("override-samples/route", default);
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        response = await Client.PatchAsync("override-samples/override/update-route", default);
        response.StatusCode.ShouldBe(HttpStatusCode.MethodNotAllowed);

        response = await Client.PostAsync("override-samples/override/update-route", default);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Action_parameter_can_be_overridden()
    {
        var response = await Client.PostAsync("override-samples/parameter", JsonContent.Create(new { parameter = "parameter" }));
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        response = await Client.PostAsync("override-samples/parameter/1", default);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<string>();
        content.ShouldBe("1");
    }

    [Test]
    public async Task Action_use_request_class_can_be_overridden()
    {
        var response = await Client.PostAsync("override-samples/request-class", new StringContent(mediaType: new("application/json"), content: """
        {
            "record":{
                "text": "text",
                "numeric": 1
            }
        }
        """));
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        response = await Client.PostAsync("override-samples/request-class", new StringContent(mediaType: new("application/json"), content: """
        {
           "text": "text",
            "numeric": 1
        }
        """));
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<Record>();
        content.ShouldDeeplyBe(new Record("text", 1));
    }
}