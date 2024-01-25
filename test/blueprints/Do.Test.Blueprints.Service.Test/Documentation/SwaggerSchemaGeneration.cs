using Do.Architecture;
using Do.Test.RestApi.Analyzer;

namespace Do.Test.Documentation;

public class SwaggerSchemaGeneration : TestServiceNfr
{
    protected override Application ForgeApplication() =>
        Forge.New
            .Service(
                business: c => c.Default(assemblies: [typeof(Entity).Assembly], controllerAssembly: typeof(ParentsController).Assembly),
                database: c => c.InMemory(),
                documentation: c => c.Default()
            );

    [Test]
    public async Task Generates_swagger_json_automatically()
    {
        var response = await Client.GetAsync("/swagger/v1/swagger.json");

        dynamic? content = await response.Content.Deserialize();

        ((string?)content?.paths["/singleton/time"].get.tags[0]).ShouldBe("Singleton");
    }
}
