using System.Net;
using System.Net.Http.Json;

namespace Do.Test.CodingStyle;

public class RoutingEntityByUniqueProperties : TestServiceNfr
{
    [Test]
    [Ignore("not implemented")]
    public async Task GetByUniqueString()
    {
        await Client.PostAsync("/entities", JsonContent.Create(
            new { unique = "test" }
        ));

        var response = await Client.GetAsync("/entities/test");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    [Ignore("not implemented")]
    public async Task GetByByUniqueEnum()
    {
        await Client.PostAsync("/entities", JsonContent.Create(
            new { @enum = "enabled" }
        ));

        var response = await Client.GetAsync("/entities/enabled");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Delete()
    {
        await Client.PostAsync("/entities", JsonContent.Create(new { unique = "test" }));

        var response = await Client.DeleteAsync($"/entities/test");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Put()
    {
        await Client.PostAsync("/entities", JsonContent.Create(new { unique = "test" }));

        var response = await Client.PutAsync($"/entities/test", JsonContent.Create(new { int32 = 42 }));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Patch()
    {
        await Client.PostAsync("/entities", JsonContent.Create(new { unique = "test" }));

        var response = await Client.PatchAsync($"/entities/test/string", JsonContent.Create(new { @string = "test" }));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Throws_not_found_when_unique_entity_doesnt_exist()
    {
        var response = await Client.DeleteAsync($"/entities/non-existing");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Test]
    [Ignore("not implemented")]
    public async Task Ignores_case_for_enum_values()
    {
        await Client.PostAsync("/entities", JsonContent.Create(new { @enum = "enabled" }));

        var response = await Client.DeleteAsync($"/entities/enabled");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}