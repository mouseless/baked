using Do.Architecture;
using Do.Greeting;

namespace Do.Test.Greeting;

public class ShowingWelcomePage : TestServiceNfr
{
    protected override Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? Greeting =>
        c => c.WelcomePage();

    [Test]
    public async Task Application_redirects_to_dotnet_welcome_page()
    {
        var response = await Client.GetAsync("/");

        var content = await response.Content.ReadAsStringAsync();

        content.ShouldContain("Your ASP.NET Core application has been successfully started");
    }
}
