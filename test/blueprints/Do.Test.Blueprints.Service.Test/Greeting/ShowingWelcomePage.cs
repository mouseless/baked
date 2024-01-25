using Do.Architecture;
using Do.Test.RestApi.Analyzer;

namespace Do.Test.Greeting;

public class ShowingWelcomePage : TestServiceNfr
{
    protected override Application ForgeApplication() =>
        Forge.New
            .Service(
                business: c => c.Default(businessAssemblies: [typeof(Entity).Assembly], applicationParts: [typeof(ParentsController).Assembly]),
                database: c => c.InMemory(),
                greeting: c => c.WelcomePage()
            );

    [Test]
    public async Task Application_redirects_to_dotnet_welcome_page()
    {
        var response = await Client.GetAsync("/");

        var content = await response.Content.ReadAsStringAsync();

        content.ShouldContain("Your ASP.NET Core application has been successfully started");
    }
}
