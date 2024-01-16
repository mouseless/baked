using Do.Architecture;

namespace Do.Test.Integration;

public class DocumentationFeatureTests : IntegrationSpec<DocumentationFeatureTests>
{
    protected override Application Application =>
        Forge.New
            .Service(
                business: c => c.Default(),
                database: c => c.MySql().ForDevelopment(c.Sqlite()),
                exceptionHandling: ex => ex.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}"),
                configure: app => app.Features.AddConfigurationOverrider()
            );

    [Test]
    public async Task Application_root_is_swagger_index_page()
    {
        var response = await Client.GetAsync("/");

        response.EnsureSuccessStatusCode();
        response.RequestMessage.ShouldNotBeNull();
        response.RequestMessage.RequestUri.ShouldBe("http://localhost/swagger/index.html");
    }
}
