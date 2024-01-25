using Do.Architecture;
using Do.Test.RestApi.Analyzer;
using System.Net;

namespace Do.Test.Configuration
{
    public class EnvironmentSwitch : TestServiceNfr
    {
        protected override Application ForgeApplication() =>
            Forge.New
                .Service(
                    business: c => c.Default(options =>
                    {
                        options.AddBusinessAssembly<Entity>();
                        options.AddApplicationPart<ParentsController>();
                    }),
                    database: c => c.InMemory(),
                    greeting: c => c.WelcomePage()
                        .ForDevelopment(c.WelcomePage("/development"))
                        .ForProduction(c.WelcomePage("/production"))
                        .ForStaging(c.WelcomePage("/staging"))
                );

        public class Development : EnvironmentSwitch
        {
            protected override string EnvironmentName => "Development";

            [Test]
            public async Task Forge_selects_the_configuration_based_on_the_environment()
            {
                (await Client.GetAsync("/development")).StatusCode.ShouldBe(HttpStatusCode.OK);
                (await Client.GetAsync("/")).StatusCode.ShouldBe(HttpStatusCode.NotFound);
            }
        }

        public class Production : EnvironmentSwitch
        {
            protected override string EnvironmentName => "Production";

            [Test]
            public async Task Forge_selects_the_configuration_based_on_the_environment()
            {
                (await Client.GetAsync("/production")).StatusCode.ShouldBe(HttpStatusCode.OK);
                (await Client.GetAsync("/")).StatusCode.ShouldBe(HttpStatusCode.NotFound);
            }
        }

        public class Staging : EnvironmentSwitch
        {
            protected override string EnvironmentName => "Staging";

            [Test]
            public async Task Forge_selects_the_configuration_based_on_the_environment()
            {
                (await Client.GetAsync("/staging")).StatusCode.ShouldBe(HttpStatusCode.OK);
                (await Client.GetAsync("/")).StatusCode.ShouldBe(HttpStatusCode.NotFound);
            }
        }
    }
}
