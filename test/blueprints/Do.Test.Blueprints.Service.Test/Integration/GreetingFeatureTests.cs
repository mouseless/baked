using Do.Architecture;

namespace Do.Test.Integration;

public class GreetingFeatureTests : IntegrationSpec<GreetingFeatureTests>
{
    protected override Application Application =>
        Forge.New
            .Service(
                business: c => c.Default(),
                exceptionHandling: ex => ex.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}"),
                greeting: c => c.WelcomePage("/WelcomePage"),
                configure: app => app.Features.AddConfigurationOverrider()
            );

    [Test]
    public async Task Application_redirects_to_dotnet_welcome_page_on_given_url()
    {
        var response = await Client.GetAsync("/WelcomePage");

        response.EnsureSuccessStatusCode();
    }
}
