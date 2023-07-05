namespace Do.Test.Architecture.Application;

public class AddingExtensions : Spec
{
    [Test]
    public void Layer_is_added_without_any_options()
    {
        var build = GiveMe.ABuild();
        var layer1 = MockMe.ALayer();
        var layer2 = MockMe.ALayer();

        var app = build.As(app =>
        {
            app.Layers.Add(layer1);
            app.Layers.Add(layer2);
        });

        app.Run();

        layer1.VerifyInitialized();
        layer2.VerifyInitialized();
    }

    [Test]
    public void Feature_is_added_to_configure_layers()
    {
        var build = GiveMe.ABuild();
        var layer = MockMe.ALayer();
        var feature1 = MockMe.AFeature();
        var feature2 = MockMe.AFeature();

        var app = build.As(app =>
        {
            app.Layers.Add(layer);

            app.Features.Add(feature1);
            app.Features.Add(feature2);
        });

        app.Run();

        feature1.VerifyInitialized();
        feature2.VerifyInitialized();
    }

    [Test]
    public void Feature_configures_target_configurations_of_the_layers()
    {
        var build = GiveMe.ABuild();
        var configurationTarget = new object();
        var layer = MockMe.ALayer(configurationTarget: configurationTarget);
        var feature = MockMe.AFeature();

        var app = build.As(app =>
        {
            app.Layers.Add(layer);

            app.Features.Add(feature);
        });

        app.Run();

        feature.VerifyConfigures(configurationTarget);
    }

    [Test]
    [Ignore("not implemented")]
    public void Layers_may_provide_multiple_configuration_targets() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void Configuration_targets_do_stuff_before_and_after_being_configured_by_features() => Assert.Fail();
}
