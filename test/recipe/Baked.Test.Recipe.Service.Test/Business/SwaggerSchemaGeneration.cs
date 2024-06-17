namespace Baked.Test.Business;

public class SwaggerSchemaGeneration : TestServiceNfr
{
    [Test]
    public async Task Generates_swagger_json_automatically()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");

        dynamic? content = await response.Content.Deserialize();

        ((string?)content?.paths["/time-provider-samples/now"].get.tags[0]).ShouldBe("TimeProviderSamples");
    }

    [Test]
    [Ignore("not implemented")]
    public void Swagger_includes_xml_documentation() => throw new("fail");
}