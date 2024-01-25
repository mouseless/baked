using Do.Architecture;
using Do.Test.RestApi.Analyzer;

namespace Do.Test.Documentation;

public class ShowingSwaggerUi : TestServiceNfr
{
    protected override Application ForgeApplication() =>
        Forge.New
            .Service(
                business: c => c.Default(assemblies: [typeof(Entity).Assembly], controllerAssembly: typeof(ParentsController).Assembly),
                database: c => c.InMemory(),
                greeting: c => c.Swagger()
            );

    [Test]
    public async Task Application_root_is_swagger_index_page()
    {
        var response = await Client.GetAsync("/");

        response.RequestMessage.ShouldNotBeNull();
        response.RequestMessage.RequestUri?.ToString().ShouldEndWith("/swagger/index.html");
    }
}
