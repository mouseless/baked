using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Do.Test.CodingStyle;

public class AddingAndRemovingChildren : TestServiceNfr
{
    [Test]
    public async Task AddChild()
    {
        var parentResponse = await Client.PostAsync("/parents", JsonContent.Create(new { }));
        dynamic? parent = JsonConvert.DeserializeObject(await parentResponse.Content.ReadAsStringAsync());

        var response = await Client.PostAsync($"/parents/{parent?.id}/children", JsonContent.Create(new { }));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task RemoveChild()
    {
        var parentResponse = await Client.PostAsync("/parents", JsonContent.Create(new { }));
        dynamic? parent = JsonConvert.DeserializeObject(await parentResponse.Content.ReadAsStringAsync());
        await Client.PostAsync($"/parents/{parent?.id}/children", JsonContent.Create(new { }));
        var childrenResponse = await Client.GetAsync($"/parents/{parent?.id}/children");
        dynamic? children = JsonConvert.DeserializeObject(await childrenResponse.Content.ReadAsStringAsync());

        var response = await Client.DeleteAsync($"/parents/{parent?.id}/children/{children?[0].id}");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}