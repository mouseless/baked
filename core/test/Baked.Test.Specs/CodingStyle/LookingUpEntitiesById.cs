using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class LookingUpEntitiesById : TestNfr
{
    [Test]
    public async Task ExposedLocate()
    {
        var entityResponse = await Client.PostAsync("/entities", JsonContent.Create(new { }));
        dynamic? entity = await entityResponse.Content.Deserialize();

        var response = await Client.GetAsync($"/entities/{entity?.id}");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task EntityParameters()
    {
        var entityResponse = await Client.PostAsync("/entities", JsonContent.Create(new { @string = "test entity" }));
        dynamic? expected = await entityResponse.Content.Deserialize();

        var response = await Client.PostAsync("/method-samples/entity-parameters", JsonContent.Create(
            new
            {
                single = new { id = $"{expected?.id}" },
                enumerable = new[] { new { id = $"{expected?.id}" }, new { id = $"{expected?.id}" } },
                array = new[] { new { id = $"{expected?.id}" }, new { id = $"{expected?.id}" } },
            }
        ));
        dynamic? actual = await response.Content.Deserialize();

        ((int?)actual?.Count).ShouldBe(5);
        $"{actual?[0].@string}".ShouldBe("test entity");
        $"{actual?[1].@string}".ShouldBe("test entity");
        $"{actual?[2].@string}".ShouldBe("test entity");
        $"{actual?[3].@string}".ShouldBe("test entity");
        $"{actual?[4].@string}".ShouldBe("test entity");
    }

    [Test]
    public async Task EntityParametersInQuery()
    {
        var entityResponse = await Client.PostAsync("/entities", JsonContent.Create(new { @string = "test entity" }));
        dynamic? expected = await entityResponse.Content.Deserialize();

        var response = await Client.GetAsync("/method-samples/entity-parameters" +
            $"?singleId={expected?.id}" +
            $"&enumerableIds={expected?.id}&enumerableIds={expected?.id}" +
            $"&arrayIds={expected?.id}&arrayIds={expected?.id}"
        );
        dynamic? actual = await response.Content.Deserialize();

        ((int?)actual?.Count).ShouldBe(5);
        $"{actual?[0].@string}".ShouldBe("test entity");
        $"{actual?[1].@string}".ShouldBe("test entity");
        $"{actual?[2].@string}".ShouldBe("test entity");
        $"{actual?[3].@string}".ShouldBe("test entity");
        $"{actual?[4].@string}".ShouldBe("test entity");
    }

    [Test]
    public async Task RecordWithEntity()
    {
        var entityResponse = await Client.PostAsync("/entities", JsonContent.Create(new { @string = "test entity" }));
        dynamic? expected = await entityResponse.Content.Deserialize();

        var response = await Client.PostAsync("/method-samples/record-with-entity", JsonContent.Create(
            new
            {
                record = new
                {
                    single = new { id = $"{expected?.id}" },
                    enumerable = new[] { new { id = $"{expected?.id}" }, new { id = $"{expected?.id}" } },
                    array = new[] { new { id = $"{expected?.id}" }, new { id = $"{expected?.id}" } },
                }
            }
        ));
        dynamic? actual = await response.Content.Deserialize();

        ((int?)actual?.Count).ShouldBe(5);
        $"{actual?[0].@string}".ShouldBe("test entity");
        $"{actual?[1].@string}".ShouldBe("test entity");
        $"{actual?[2].@string}".ShouldBe("test entity");
        $"{actual?[3].@string}".ShouldBe("test entity");
        $"{actual?[4].@string}".ShouldBe("test entity");
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