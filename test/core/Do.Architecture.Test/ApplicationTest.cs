using Do.Architecture;

namespace Do.Test;

[TestFixture]
public class ApplicationTest
{
    [Test]
    public void Is_built_through_the_builder()
    {
        var actual = Build.Application.As(app => { });

        Assert.That(actual, Is.InstanceOf<IRunnable>());
    }

    [Test]
    public void Prints_banner_prior_to_build()
    {
        Assert.Fail("not implemented");
    }

    [Test]
    public void Allows_to_add_a_layer()
    {
        Assert.Fail("not implemented");
    }

    [Test]
    public void Allows_to_add_a_feature()
    {
        Assert.Fail("not implemented");
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
