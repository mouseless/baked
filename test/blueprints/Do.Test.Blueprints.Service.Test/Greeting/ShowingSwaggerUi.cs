using Do.Architecture;
using Do.Greeting;

namespace Do.Test.Documentation;

public class ShowingSwaggerUi : TestServiceNfr
{
    protected override Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? Greeting =>
        c => c.Swagger();

    [Test]
    public async Task Application_root_is_swagger_index_page()
    {
        var response = await Client.GetAsync("/");

        response.RequestMessage.ShouldNotBeNull();
        response.RequestMessage.RequestUri?.ToString().ShouldEndWith("/swagger/index.html");
    }
}
