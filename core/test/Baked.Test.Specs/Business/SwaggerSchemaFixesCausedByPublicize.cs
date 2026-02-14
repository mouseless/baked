namespace Baked.Test.Business;

public class SwaggerSchemaFixesCausedByPublicize : TestNfr
{
    [Test]
    public async Task Unused_schemas_are_removed()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");
        dynamic? content = await response.Content.Deserialize();

        ((object?)content?.components.schemas["Assembly"]).ShouldBeNull();
    }

    [Test]
    public async Task Non_public_properties_removed_from_swagger()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");
        dynamic? content = await response.Content.Deserialize();

        ((object?)content?.components.schemas["Record"].properties["internal"]).ShouldBeNull();
        ((object?)content?.components.schemas["Record"].properties["equalityContract"]).ShouldBeNull();
    }
}