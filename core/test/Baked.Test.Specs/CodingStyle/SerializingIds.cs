using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class SerializingIds : TestNfr
{
    [Test]
    public async Task Serializes_id_using_property_name_for_entities()
    {
        var response = await Client.PostAsync("/entity-with-auto-increment-ids", JsonContent.Create(new { }));
        dynamic? entity = await response.Content.Deserialize();

        response = await Client.PostAsync($"/entity-with-auto-increment-ids/{entity?.primaryKey}/test-custom-id-property-name", JsonContent.Create(
            new
            {
                other = new { primaryKey = $"{entity?.primaryKey}" }
            }
        ));
        dynamic? actual = await response.Content.Deserialize();

        ((string?)actual?.primaryKey).ShouldBe($"{entity?.primaryKey}");
    }

    [Test]
    public async Task Id_suffix_uses_property_name_for_entities()
    {
        var response = await Client.PostAsync("/entity-with-auto-increment-ids", JsonContent.Create(new { }));
        dynamic? entity = await response.Content.Deserialize();

        response = await Client.GetAsync($"/entity-with-auto-increment-ids/{entity?.primaryKey}/test-custom-id-property-name?otherPrimaryKey={entity?.primaryKey}");
        dynamic? actual = await response.Content.Deserialize();

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
        dynamic? actual = await response.Content.Deserialize();

        ((string?)actual?.uid).ShouldBe("test-uid");
    }

    [Test]
    public async Task Id_suffix_uses_property_name_for_rich_transients()
    {
        var response = await Client.GetAsync($"/rich-transient-with-custom-id-properties/test-uid/test-custom-id-property-name?otherUid=test-uid");
        dynamic? actual = await response.Content.Deserialize();

        ((string?)actual?.uid).ShouldBe("test-uid");
    }

    [Test]
    public async Task Non_id_properties_are_readonly()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");
        dynamic? content = await response.Content.Deserialize();

        dynamic? entity = content?.components.schemas["Entity"];
        ((object?)entity).ShouldNotBeNull();
        ((bool?)entity.properties["id"].readOnly).ShouldNotBe(true);
        ((bool?)entity.properties["string"].readOnly).ShouldBe(true);
        ((bool?)entity.properties["enum"].readOnly).ShouldBe(true);
    }
}