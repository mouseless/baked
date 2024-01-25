using Do.Architecture;
using Do.Greeting;
using System.Net;

namespace Do.Test.Configuration;

[TestFixture("Development", "/development")]
[TestFixture("Staging", "/staging")]
[TestFixture("Production", "/production")]
public class EnvironmentSwitch(string _environment, string _path) : TestServiceNfr
{
    public EnvironmentSwitch() : this(default!, default!) { }

    protected override string EnvironmentName => _environment;
    protected override Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? Greeting =>
        c => c.WelcomePage()
            .ForDevelopment(c.WelcomePage("/development"))
            .ForStaging(c.WelcomePage("/staging"))
            .ForProduction(c.WelcomePage("/production"))
        ;

    [Test]
    public async Task Forge_selects_the_configuration_based_on_the_environment()
    {
        (await Client.GetAsync(_path)).StatusCode.ShouldBe(HttpStatusCode.OK);
        (await Client.GetAsync("/")).StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
