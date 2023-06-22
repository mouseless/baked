using Do.Architecture;
using Do.Branding;

namespace Do.Test.Architecture;

public class BuildingAnApplication : Spec
{
    [Test]
    public void Build_is_accessible_via_a_fluent_api()
    {
        Assert.That(Build.Application, Is.InstanceOf<Build>());
    }

    [Test]
    public void Application_is_built_through_the_builder()
    {
        var build = GiveMe.ABuild();

        var actual = build.As(_ => { });

        Assert.That(actual, Is.InstanceOf<IRunnable>());
    }

    [Test]
    public void It_prints_banner_prior_to_build()
    {
        var mockBanner = new Mock<IBanner>();
        var build = GiveMe.ABuild(banner: mockBanner.Object);

        build.As(_ => { });

        mockBanner.Verify(b => b.Print());
    }

    [Test]
    [Ignore("not implemented")]
    public void Allows_to_add_a_layer()
    {
        Assert.Fail();
    }

    [Test]
    [Ignore("not implemented")]
    public void Allows_to_add_a_feature()
    {
        Assert.Fail();
    }
}

/*

Build.Application
    .As(app =>
    {
        app.Layers.AddDomain();
        app.Layers.AddMonitoring();
        app.Layers.AddRdbms();

        app.Features.AddAuthentication(c => c.JwtBearer());
        app.Features.AddLogging(c => c.Default());
        app.Features.AddDomainObjects(c => c.UseAssemblies("Do.Domain.Test*"));
    })
    .Run();

*/
