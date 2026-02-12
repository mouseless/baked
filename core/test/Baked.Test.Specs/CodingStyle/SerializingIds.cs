using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class SerializingIds : TestNfr
{
    [Test]
    public async Task Serializes_id_using_property_name_for_entities()
    {
        var response = await Client.PostAsync("/entity-with-auto-increment-ids", JsonContent.Create(new { }));
        dynamic? entity = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        response = await Client.PostAsync($"/entity-with-auto-increment-ids/{entity?.primaryKey}/test-custom-id-property-name", JsonContent.Create(
            new
            {
                other = new { primaryKey = $"{entity?.primaryKey}" }
            }
        ));
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((string?)actual?.primaryKey).ShouldBe($"{entity?.primaryKey}");
    }

    [Test]
    public async Task Id_suffix_uses_property_name_for_entities()
    {
        var response = await Client.PostAsync("/entity-with-auto-increment-ids", JsonContent.Create(new { }));
        dynamic? entity = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        response = await Client.GetAsync($"/entity-with-auto-increment-ids/{entity?.primaryKey}/test-custom-id-property-name?otherPrimaryKey={entity?.primaryKey}");
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((string?)actual?.primaryKey).ShouldBe($"{entity?.primaryKey}");
    }

    [Test]
    public async Task Serializes_id_using_property_name_for_rich_transients()
    {
        var response = await Client.PostAsync("/rich-transient-with-custom-id-properties/test-uid/test-custom-id-property-name", JsonContent.Create(
            new
            {
                other = new { uid = "test-uid" }
            }
        ));
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((string?)actual?.uid).ShouldBe("test-uid");
    }

    [Test]
    public async Task Id_suffix_uses_property_name_for_rich_transients()
    {
        var response = await Client.GetAsync($"/rich-transient-with-custom-id-properties/test-uid/test-custom-id-property-name?otherUid=test-uid");
        dynamic? actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        ((string?)actual?.uid).ShouldBe("test-uid");
    }
}