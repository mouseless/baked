namespace Baked.Test.Business;

public class SwaggerSchemaGeneration : TestServiceNfr
{
    [Test]
    public async Task Generates_swagger_json_automatically()
    {
        var response = await Client.GetAsync("/swagger/v1/swagger.json");

        dynamic? content = await response.Content.Deserialize();

        ((string?)content?.paths["/time-provider-samples/now"].get.tags[0]).ShouldBe("TimeProviderSamples");
    }
}