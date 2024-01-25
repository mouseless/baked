using Do.Architecture;
using Do.Documentation;

namespace Do.Test.Documentation;

public class SwaggerSchemaGeneration : TestServiceNfr
{
    protected override Func<DocumentationConfigurator, IFeature<DocumentationConfigurator>>? Documentation =>
        c => c.Default();

    [Test]
    public async Task Generates_swagger_json_automatically()
    {
        var response = await Client.GetAsync("/swagger/v1/swagger.json");

        dynamic? content = await response.Content.Deserialize();

        ((string?)content?.paths["/singleton/time"].get.tags[0]).ShouldBe("Singleton");
    }
}
