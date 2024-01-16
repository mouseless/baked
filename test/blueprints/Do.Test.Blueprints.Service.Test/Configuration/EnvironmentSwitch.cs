using Do.Architecture;
using System.Net;

namespace Do.Test.Configuration
{
    public class EnvironmentSwitch : TestServiceNfr
    {
        protected override string EnvironmentName => "Development";

        protected override Application ForgeApplication() =>
            Forge.New
                .Service(
                    business: c => c.Default(),
                    database: c => c.InMemory(),
                    greeting: c => c.WelcomePage().ForDevelopment(c.WelcomePage("/welcome"))
                );

        [Test]
        public async Task Forge_selects_the_configuration_based_on_the_environment()
        {
            (await Client.GetAsync("/welcome")).StatusCode.ShouldBe(HttpStatusCode.OK);
            (await Client.GetAsync("/")).StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
