namespace Do.Test.Architecture.Application;

public class AddingFeatures : Spec
{
    [Test]
    public void Builder_allows_to_add_a_new_layer()
    {
        var build = GiveMe.ABuild();

        var app = build.As(app =>
        {
            app.Layers.AddCustom();
        });

        app.Run();
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
        app.Features.AddAuthentication(c => c.JwtBearer());
        app.Features.AddLogging(c => c.Default());
        app.Features.AddDomainObjects(c => c.UseAssemblies("Do.Domain.Test*"));
    })
    .Run();
*/
