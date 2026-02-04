using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class RoutingLocatableExtensions : TestNfr
{
    [Test]
    public async Task Entity_extensions_are_served_under_same_routes()
    {
        var entityResponse = await Client.PostAsync("/entities", JsonContent.Create(new { }));
        dynamic? entity = JsonConvert.DeserializeObject(await entityResponse.Content.ReadAsStringAsync());

        var response = await Client.PostAsync($"/entities/{entity?.id}/increment-int32", new StringContent(string.Empty));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Rich_transient_extensions_are_served_under_same_routes()
    {
        var response = await Client.PostAsync($"/rich-transient-with-datas/{12}/from-extension", new StringContent(string.Empty));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Extensions_can_be_used_as_parameters_just_like_locatables()
    {
        var entityResponse = await Client.PostAsync("/entities", JsonContent.Create(new { int32 = 1 }));
        dynamic? entity = JsonConvert.DeserializeObject(await entityResponse.Content.ReadAsStringAsync());

        var response = await Client.PostAsync($"/entities/{entity?.id}/increment-by", JsonContent.Create(
            new { otherId = $"{entity?.id}" }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Extensions_as_enumerable_parameters()
    {
        var entityResponse = await Client.PostAsync("/entities", JsonContent.Create(new { int32 = 1 }));
        dynamic? entity = JsonConvert.DeserializeObject(await entityResponse.Content.ReadAsStringAsync());

        var response = await Client.PostAsync($"/entities/{entity?.id}/increment-by-all", JsonContent.Create(
            new
            {
                extensionIds = new[] { $"{entity?.id}" },
                moreExtensionIds = new[] { $"{entity?.id}" },
                evenMoreExtensionIds = new[] { $"{entity?.id}" }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}