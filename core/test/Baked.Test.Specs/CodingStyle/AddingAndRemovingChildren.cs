using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class AddingAndRemovingChildren : TestNfr
{
    [Test]
    public async Task AddChild()
    {
        var parentResponse = await Client.PostAsync("/parents", JsonContent.Create(new { name = "test", surname = "test" }));
        dynamic? parent = await parentResponse.Content.Deserialize();

        var response = await Client.PostAsync($"/parents/{parent?.id}/children", JsonContent.Create(new { name = "child" }));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task RemoveChild()
    {
        var parentResponse = await Client.PostAsync("/parents", JsonContent.Create(new { name = "test", surname = "test" }));
        dynamic? parent = await parentResponse.Content.Deserialize();
        await Client.PostAsync($"/parents/{parent?.id}/children", JsonContent.Create(new { name = "child" }));
        var childrenResponse = await Client.GetAsync($"/parents/{parent?.id}/children");
        dynamic? children = await childrenResponse.Content.Deserialize();

        var response = await Client.DeleteAsync($"/parents/{parent?.id}/children/{children?[0].id}");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}