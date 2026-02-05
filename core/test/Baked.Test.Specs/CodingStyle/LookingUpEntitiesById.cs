using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class LookingUpEntitiesById : TestNfr
{
    [Test]
    public async Task ExposedSingleById()
    {
        var entityResponse = await Client.PostAsync("/entities", JsonContent.Create(new { }));
        dynamic? entity = JsonConvert.DeserializeObject(await entityResponse.Content.ReadAsStringAsync());

        var response = await Client.GetAsync($"/entities/{entity?.id}");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task EntityParameters()
    {
        var entityResponse = await Client.PostAsync("/entities", JsonContent.Create(new { }));
        dynamic? expected = JsonConvert.DeserializeObject(await entityResponse.Content.ReadAsStringAsync());

        var response = await Client.PostAsync("/method-samples/entity-parameters", JsonContent.Create(
            new
            {
                entityId = $"{expected?.id}"
            }
        ));
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        $"{actual?.id}".ShouldBe($"{expected?.id}");
    }

    [Test]
    public async Task EntityListParameters()
    {
        var entityResponse = await Client.PostAsync("/entities", JsonContent.Create(new { }));
        dynamic? expected = JsonConvert.DeserializeObject(await entityResponse.Content.ReadAsStringAsync());

        var response = await Client.PostAsync("/method-samples/entity-list-parameters", JsonContent.Create(
            new
            {
                entityIds = new[] { $"{expected?.id}", $"{expected?.id}" },
                otherEntityIds = new[] { $"{expected?.id}", $"{expected?.id}" },
            }
        ));
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((int?)actual?.Count).ShouldBe(4);
        $"{actual?[0].id}".ShouldBe($"{expected?.id}");
    }

    [Test]
    public async Task RecordWithEntity()
    {
        var entityResponse = await Client.PostAsync("/entities", JsonContent.Create(new { }));
        dynamic? expected = JsonConvert.DeserializeObject(await entityResponse.Content.ReadAsStringAsync());

        var response = await Client.PostAsync("/method-samples/record-with-entity", JsonContent.Create(
            new
            {
                record = new
                {
                    entities = new[] { new { id = $"{expected?.id}" }, new { id = $"{expected?.id}" } },
                    otherEntities = new[] { new { id = $"{expected?.id}" }, new { id = $"{expected?.id}" } },
                }
            }
        ));
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((int?)actual?.Count).ShouldBe(4);
        $"{actual?[0].id}".ShouldBe($"{expected?.id}");
    }

    [Test]
    public async Task Ignore_required_properties_for_input()
    {
        var parent = await Client.PostParents(name: "parent");
        await Client.PostParentsChildren(id: (string)parent.id);
        var children = await Client.GetParentsChildren((string)parent.id);

        var response = await Client.PutAsync($"/children/{children[0].id}", JsonContent.Create(
            new { parentWrapper = new { parent = new { id = (string)parent.id } } }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}