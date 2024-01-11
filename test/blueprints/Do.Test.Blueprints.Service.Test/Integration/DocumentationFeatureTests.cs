using Do.Architecture;

namespace Do.Test.Integration;

public class DocumentationFeatureTests : IntegrationSpec
{
    [Test]
    public async Task Application_root_is_swagger_index_page()
    {
        var client = Factory.CreateClient();

        var response = await client.GetAsync("/");

        response.EnsureSuccessStatusCode();
        response.RequestMessage.ShouldNotBeNull();
        response.RequestMessage.RequestUri.ShouldBe("http://localhost/swagger/index.html");
    }
}
