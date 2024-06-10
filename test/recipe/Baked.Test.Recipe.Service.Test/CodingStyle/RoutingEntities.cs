using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Do.Test.CodingStyle;

public class RoutingEntities : TestServiceNfr
{
    [Test]
    public async Task Post()
    {
        var response = await Client.PostAsync("/entities", JsonContent.Create(
            new { @string = "test" }
        ));

        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((string?)actual?.id).ShouldNotBeNull();
        ((string?)actual?.@string).ShouldBe("test");
    }

    [Test]
    public async Task Get()
    {
        await Client.PostAsync("/entities", JsonContent.Create(new { @string = "right" }));
        await Client.PostAsync("/entities", JsonContent.Create(new { @string = "wrong" }));

        var response = await Client.GetAsync("/entities?string=right");

        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((int?)actual?.Count).ShouldBe(1);
        ((string?)actual?[0].@string).ShouldBe("right");
    }

    [Test]
    public async Task Delete()
    {
        var entityResponse = await Client.PostAsync("/entities", JsonContent.Create(new { }));
        dynamic? entity = JsonConvert.DeserializeObject(await entityResponse.Content.ReadAsStringAsync());

        var response = await Client.DeleteAsync($"/entities/{entity?.id}");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Put()
    {
        var entityResponse = await Client.PostAsync("/entities", JsonContent.Create(new { }));
        dynamic? entity = JsonConvert.DeserializeObject(await entityResponse.Content.ReadAsStringAsync());

        var response = await Client.PutAsync($"/entities/{entity?.id}", JsonContent.Create(new { int32 = 42 }));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Patch()
    {
        var entityResponse = await Client.PostAsync("/entities", JsonContent.Create(new { }));
        dynamic? entity = JsonConvert.DeserializeObject(await entityResponse.Content.ReadAsStringAsync());

        var response = await Client.PatchAsync($"/entities/{entity?.id}/string", JsonContent.Create(new { @string = "test" }));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}