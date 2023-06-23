namespace Do.Test.Architecture.Application;

public class AddingExtensions : Spec
{
    [Test]
    public void Builder_allows_to_add_a_new_layer()
    {
        var mockLayer1 = MockMe.ALayer();
        var mockLayer2 = MockMe.ALayer();
        var build = GiveMe.ABuild();

        var app = build.As(app =>
        {
            app.Layers.Add(mockLayer1.Object);
            app.Layers.Add(mockLayer2.Object);
        });

        app.Run();

        mockLayer1.VerifyInitialized();
        mockLayer2.VerifyInitialized();
    }

    [Test]
    [Ignore("not implemented")]
    public void Allows_to_add_a_feature()
    {
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

        Assert.Fail();
    }
}
